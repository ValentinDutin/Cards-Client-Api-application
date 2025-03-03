using Microsoft.Extensions.Configuration;

namespace CommonFiles.Services
{
    public class ConfigDataService : IConfigDataService
    {
        private IConfiguration _config;
        public ConfigDataService()
        {
            try
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile("commonappsettings.json", optional: false)
                    .Build();
            }
            catch
            {
                throw new Exception("ConfigData.Constructor : ConfigurationBuilder is not build");
            }
        }
        public string GetData(string key)
        {
            string? result = _config[key];
            if (String.IsNullOrEmpty(result))
                throw new Exception($"ConfigData.GetData : Not found {key}");
            return result;
        }
    }
}

