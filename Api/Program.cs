using System.Reflection;
using ServiceLayer.AppConfig;
using WebApiLayer.Configurations;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var configuration = builder.Configuration;
#region Add configurations to Services
{
    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
    services.AddDbServices();
    services.AddAppServices();
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
}
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAutoWrapper();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
