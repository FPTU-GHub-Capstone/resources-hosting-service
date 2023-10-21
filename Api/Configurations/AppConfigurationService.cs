using AutoWrapper;
using DomainLayer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using Serilog;
using Serilog.Events;
using Serilog.Settings.Configuration;
using ServiceLayer.Business;
using ServiceLayer.Core.AppConfig;

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
            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(
                        settings.Value.ConnectionStrings.DefaultConnection
                    )
            );
            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetService<ApplicationDbContext>()
            );
        }

        public static WebApplication UseAutoWrapper(this WebApplication app)
        {
            app.UseApiResponseAndExceptionWrapper(
                new AutoWrapperOptions
                {
                    IsApiOnly = false,
                    ShowIsErrorFlagForSuccessfulResponse = true,
                    WrapWhenApiPathStartsWith = $"/{Constants.HTTP.API_VERSION}",
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
    }
}
