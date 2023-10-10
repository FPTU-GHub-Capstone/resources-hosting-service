using AutoWrapper;
using DomainLayer.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using ServiceLayer.AppConfig;
using ServiceLayer.Business;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApiLayer.Filters;

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
            services.AddScoped<IAssetAttributeServices, AssetAttributeServices>();
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
        public static void AddValidationServices(this IServiceCollection services)
        {
            services
            .AddControllers(
                options =>
                {
                    options.SuppressAsyncSuffixInActionNames = false;
                    options.Filters.Add<ValidateModelStateFilter>();
                }
            )
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }
            );
        }
    }
}
