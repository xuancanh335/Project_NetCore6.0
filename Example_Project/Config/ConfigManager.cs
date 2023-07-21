using Example_Project.Config.Abtracts;
using Microsoft.Extensions.Configuration;

namespace Example_Project.Config
{
    public class ConfigManager : IConfigManager
    {
        public static IConfiguration _config = ConfigContainerDJ.CreateInstance<IConfiguration>();

        public static string StaticGet(string nameConfig)
        {
            return _config.GetSection(nameConfig).Value;
        }

        public string Get(string nameConfig)
        {
            return _config.GetSection(nameConfig).Value;
        }
    }
}
