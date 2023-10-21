using System.Reflection;
using Serilog;
using ServiceLayer.Core.AppConfig;
using WebApiLayer.Configurations;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var configurationManager = builder.Configuration;
#region Add configurations to Services
{
    var configuration = configurationManager.GetSection(nameof(AppSettings));
    services.Configure<AppSettings>(configuration);
    builder.UseSerilog();
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

app.UseLoggingInterceptor();
app.UseAutoWrapper();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
