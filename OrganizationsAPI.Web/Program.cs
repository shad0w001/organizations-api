using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Appllication.Interfaces.UserServices;
using OrganizationsAPI.Appllication.Services;
using OrganizationsAPI.Appllication.Services.UserServices;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using OrganizationsAPI.Infrastructure.DbContext;
using OrganizationsAPI.Infrastructure.DbManager;
using OrganizationsAPI.Infrastructure.Repositories;
using OrganizationsAPI.Infrastructure.Authentication;
using OrganizationsAPI.Web.OptionsSetup;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using OrganizationsAPI.Infrastructure.Authorization;
using OrganizationsAPI.Infrastructure.Authorization.PermissionService;
using System.Net;
using OrganizationsAPI.Infrastructure.PdfGenerator;
using Quartz;
using OrganizationsAPI.Appllication.MiddleWare;
using OrganizationsAPI.Infrastructure.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JWTOptionsSetup>();
builder.Services.ConfigureOptions<JWTBearerOptionsSetup>();
builder.Services.ConfigureOptions<RequestInfoOptionsSetup>();

builder.Services.AddSingleton<DapperContext>();
DbManager.EnsureDatabaseExistsAsync(
    builder.Configuration.GetConnectionString("SqlServer"),
    builder.Configuration.GetConnectionString("Sqlite"));

builder.Services.AddSingleton<IRequestTracker, RequestTracker>();

builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();

builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<IOrganizationsService, OrganizationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IPasswordManager, PasswordManager>();
builder.Services.AddTransient<IJWTProvider, JWTProvider>();

builder.Services.AddTransient<IPdfGenerator , PdfGenerator>();

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey(nameof(WriteRequestDataToJsonJob));

    options.AddJob<WriteRequestDataToJsonJob>(jobKey).AddTrigger(
        trigger => trigger.ForJob(jobKey)
            .WithCronSchedule("* * * * * ? *"));
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestTrackingMiddleware>();

app.MapControllers();

app.Use(async (context, next) =>
{
    var remoteIpAddress = context.Connection.RemoteIpAddress;

    if (IPAddress.IsLoopback(remoteIpAddress) || remoteIpAddress.ToString() == "127.0.0.1")
    {
        context.Response.Headers.Add("sender", "organization-api");
        await next.Invoke();
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Access Forbidden");
    }
});

app.Run();
