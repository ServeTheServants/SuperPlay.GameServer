using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>((opts) => opts.UseSqlite(connectionString));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}