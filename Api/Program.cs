using Application.Interfaces;
using Application.Services.ActivityServices;
using Infrastructure.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Api.Configurations;
using Application.AppConfig;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
#region Add configurations to Services
{
    var services = builder.Services;
    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
    services.AddAppServices();
    services.AddDbServices();
}
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
