using Application.Interfaces;
using Application.Services.ActivityServices;
using Infrastructure.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer("name=ConnectionStrings:GHub_Connection", b=>b.MigrationsAssembly("Infrastructure")));

#region Add configurations to Services
var services = builder.Services;
services.AddAppServices();
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
