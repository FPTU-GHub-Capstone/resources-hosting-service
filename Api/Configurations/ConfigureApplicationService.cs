using Application.Interfaces;
using Application.Services.ActivityServices;
using Application.Services.AssetServices;
using Application.Services.AttributeServices;
using Application.Services.CharacterServices;
using Application.Services.GameServices;
using Application.Services.LevelServices;
using Application.Services.PaymentServices;
using Application.Services.TransactionServices;
using Application.Services.UserServices;
using Application.Services.WalletServices;

namespace Api.Configurations
{
    public static class ConfigureApplicationService
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IActivityServices, ActivityServices>();
            services.AddScoped<IAssetServices, AssetServices>();
            services.AddScoped<IAttributeServices, AttributeServices>();
            services.AddScoped<ICharacterServices, CharacterServices>();
            services.AddScoped<IGameServices, GameServices>();
            services.AddScoped<ILevelServices, LevelServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWalletServices, WalletServices>();
        }
    }
}
