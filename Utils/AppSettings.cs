using Microsoft.Extensions.Configuration;
using System.IO;

namespace YourProject.Configuration
{
    public static class AppSettings
    {
        private static IConfigurationRoot _config;

        static AppSettings()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string BaseUrl => _config["TestEnvironment:BaseUrl"];
        public static string Username => _config["Credentials:Username"];
        public static string Password => _config["Credentials:Password"];
    }
}
