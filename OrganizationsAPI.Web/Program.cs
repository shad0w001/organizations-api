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

builder.Services.AddSingleton<DapperContext>();
DbManager.EnsureDatabaseExistsAsync(
    builder.Configuration.GetConnectionString("SqlServer"),
    builder.Configuration.GetConnectionString("Sqlite"));

builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();

builder.Services.AddTransient<IOrganizationsService, OrganizationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPasswordManager, PasswordManager>();
builder.Services.AddTransient<IJWTProvider, JWTProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
