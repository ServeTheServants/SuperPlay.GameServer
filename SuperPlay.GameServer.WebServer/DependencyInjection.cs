using SuperPlay.GameServer.WebServer;
using SuperPlay.GameServer.WebServer.Infrastructure.Logger;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddWebServerServices(this IServiceCollection services)
        {
            services.AddTransient<WebSocketLogger>();

            services.AddTransient<WebSocketRequestHandler>();

            return services;
        }
    }
}