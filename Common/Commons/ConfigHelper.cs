using Microsoft.Extensions.Configuration;

namespace Common.Commons
{
    public class ConfigHelper
    {
        private static string jsonFileName = "appsettings.json";
        public static string Get(string nameConfig)
        {
            return new ConfigurationBuilder().AddJsonFile(jsonFileName).Build().GetSection(nameConfig).Value;
        }

        public static string Get(string nameConfig, string key)
        {
            return new ConfigurationBuilder().AddJsonFile(jsonFileName).Build().GetSection(nameConfig)[key];
        }
    }
}
