using AutoWrapper;
using DomainLayer.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using Serilog;
using Serilog.Events;
using ServiceLayer.Business;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApiLayer.Configurations.AppConfig;


namespace WebApiLayer.Configurations
{
    public static class AppConfigurationService
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IActivityServices, ActivityServices>();
            services.AddScoped<IActivityTypeServices, ActivityTypeServices>();
            services.AddScoped<IAssetAttributeServices, AssetAttributeServices>();
            services.AddScoped<IAssetServices, AssetServices>();
            services.AddScoped<IAssetTypeServices, AssetTypeServices>();
            services.AddScoped<IAttributeGroupServices, AttributeGroupServices>();
            services.AddScoped<ICharacterAssetServices, CharacterAssetServices>();
            services.AddScoped<ICharacterAttributeServices, CharacterAttributeServices>();
            services.AddScoped<ICharacterServices, CharacterServices>();
            services.AddScoped<ICharacterTypeServices, CharacterTypeServices>();
            services.AddScoped<IGameServerServices, GameServerServices>();
            services.AddScoped<IGameServices, GameServices>();
            services.AddScoped<IGameUserServices, GameUserServices>();
            services.AddScoped<ILevelProgressServices, LevelProgressServices>();
            services.AddScoped<ILevelServices, LevelServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWalletCategoryServices, WalletCategoryServices>();
            services.AddScoped<IWalletServices, WalletServices>();
        }

        public static void AddDbServices(this IServiceCollection services)
        {
            var settings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
            Console.WriteLine(settings);
            // use Console buildin logger to prevent EF log write to DB stream
            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseSqlServer(settings.Value.ConnectionStrings.DefaultConnection)
                    .LogTo(Console.WriteLine,
                        new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information,
                        DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.UtcTime)
            );
            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetService<ApplicationDbContext>()
            );
        }

        public static void AddCorsMechanism(this IServiceCollection services)
        {
            services.AddCors(p => p.AddPolicy(Constants.Http.CORS, build =>
            {
                build.WithOrigins("*")
                     .AllowAnyMethod()
                     .AllowAnyHeader();
            }));
        }

        public static WebApplication UseAutoWrapper(this WebApplication app)
        {
            app.UseApiResponseAndExceptionWrapper(
                new AutoWrapperOptions
                {
                    IsApiOnly = false,
                    ShowIsErrorFlagForSuccessfulResponse = true,
                    WrapWhenApiPathStartsWith = $"/{Constants.Http.API_VERSION}",
                }
            );
            return app;
        }

        public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder, IConfiguration configuration) {
            builder.Host.UseSerilog((cntxt, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(configuration);
            });
            return builder;
        }
        
        public static WebApplication UseLoggingInterceptor(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });
            return app;
        }
        
        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        
        public static async Task DbInitializer(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await DatabaseInitializer.InitializeAsync(dbContext);
        }

        public static IServiceCollection AddAppAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options => {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWTOptions.Secret)),
                    LifetimeValidator = CustomLifetimeValidator(),
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception}");
                        return Task.CompletedTask;
                    },
                };
            });
            return services;
        }

        private static LifetimeValidator CustomLifetimeValidator()
        {
            static bool lifetimeValidator(DateTime? _notBefore, DateTime? _expires, SecurityToken securityToken, TokenValidationParameters _validationParameters)
            {
                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    var expirationClaim = (long?)jwtSecurityToken.Payload["exp"];
                    if (expirationClaim == null)
                    {
                        return false;
                    }
                    return expirationClaim > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                }
                return false;
            }
            return lifetimeValidator;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Game Management Services",
                    Version = "v1"
                });
                OpenApiSecurityScheme securityDefinition = new()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new() {
            {
                securityScheme,
                new string[] { }
            },
        };
                c.AddSecurityRequirement(securityRequirements);
            });
            return services;
        }
    }
}
