using Microsoft.Extensions.Configuration;
using System;

namespace Core.Configuration
{
    public class ConfigurationSettings
    {
        private static readonly string Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
        private static IConfigurationRoot Configuration { get; set; }
        protected readonly AppSettings _appSettings;

        static ConfigurationSettings()
        {
            Configuration = LoadAppSettings();
        }
        public ConfigurationSettings(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public static AppSettings GetAppSettings()
        {
            return Configuration.Get<AppSettings>();
        }
        private static IConfigurationRoot LoadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Env}.json", optional: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}

