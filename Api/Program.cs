using Serilog;
using System.Reflection;
using DomainLayer.Constants;
using WebApiLayer.Configurations;
using WebApiLayer.Configurations.AppConfig;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

#region Add configurations to Services
{
    var configurationManager = builder.Configuration;
    var configuration = configurationManager.GetSection(nameof(AppSettings));
    services.Configure<AppSettings>(configuration);
    builder.UseSerilog(configuration);
    services.AddDbServices();
    services.AddAppServices();
    services.AddCorsMechanism();
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
}
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.Services.ApplyMigrations();
    await app.Services.DbInitializer();
}

app.UseLoggingInterceptor();
app.UseCors(Constants.HTTP.CORS);
app.UseAutoWrapper();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
