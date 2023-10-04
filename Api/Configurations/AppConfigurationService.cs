﻿using Application.AppConfig;
using Application.Interfaces;
using Application.Interfaces.Activity;
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
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Configurations
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
            services.AddScoped<IAttributeServices, AttributeServices>();
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
    }
}