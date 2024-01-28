using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Appllication.Services;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using OrganizationsAPI.Infrastructure.DbContext;
using OrganizationsAPI.Infrastructure.DbManager;
using OrganizationsAPI.Infrastructure.Repositories;
using OrganizationsAPI.Web.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JWTOptionsSetup>();
builder.Services.ConfigureOptions<JWTBearerOptionsSetup>();

builder.Services.AddSingleton<DapperContext>();
DbManager.EnsureDatabaseExistsAsync(
    builder.Configuration.GetConnectionString("SqlServer"),
    builder.Configuration.GetConnectionString("Sqlite"));

builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>();

builder.Services.AddTransient<IOrganizationsService, OrganizationService>();

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
