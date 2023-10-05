using ServiceLayer.AppConfig;
using WebApiLayer.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
#region Add configurations to Services
{
    var services = builder.Services;
    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
    services.AddDbServices();
    services.AddAppServices();
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
