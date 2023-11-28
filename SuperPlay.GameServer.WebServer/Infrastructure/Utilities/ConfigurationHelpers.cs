using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SuperPlay.GameServer.WebServer.Infrastructure.Utilities
{
	public class ConfigurationHelpers
	{
        public static IConfigurationRoot BuildConfiguration(IWebHostEnvironment env) =>
            new ConfigurationBuilder()
                .AddJsonFile(GetAppSettingsFileName(env), optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        public static string GetAppSettingsFileName(IWebHostEnvironment env)
        {
            var environmentName = env.EnvironmentName.ToLower();

            return environmentName == "local"
                ? "appsettings.json"
                : $"appsettings.{environmentName}.json";
        }
    }
}