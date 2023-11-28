using System.Text.Json;
using SuperPlay.GameServer.Application.Common.Containers;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Application.Login;
using SuperPlay.GameServer.Application.SendGift;
using SuperPlay.GameServer.Application.UpdateResources;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            services.AddTransient<SendGiftRequestHandler>();

            services.AddTransient<LoginRequestHandler>();

            services.AddTransient<UpdateResourcesRequestHandler>();

            services.AddSingleton<IPlayerConnectionContainer, PlayerConnectionContainer>();

            return services;
        }
    }
}