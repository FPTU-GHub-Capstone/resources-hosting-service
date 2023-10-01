using Application.Interfaces;
using Application.Services.ActivityServices;
using Infrastructure.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer("name=ConnectionStrings:GHub_Connection", b=>b.MigrationsAssembly("Infrastructure")));

//-------------------------------
//Add configurations to Services
var services = builder.Services;
services.AddAppServices();
services.AddRepositories();
//End of configurations
//-------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
