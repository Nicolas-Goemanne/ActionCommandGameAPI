using ActionCommandGame.Sdk.Handlers;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace ActionCommandGame.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
        {
            services.AddScoped<AuthorizationHandler>();

            services.AddHttpClient("ActionCommandGameApi", options =>
            {
                options.BaseAddress = new Uri(apiUrl);
            });

            services.AddScoped<PositiveGameEventSdk>();
            services.AddScoped<NegativeGameEventSdk>();
            services.AddScoped<PlayerItemSdk>();
            services.AddScoped<GameSdk>();
            services.AddScoped<ItemSdk>();
            services.AddScoped<PlayerSdk>();
            

            return services;
        }
    }
}
