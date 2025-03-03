using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class ConfigData : IConfigData
    {
        private IConfiguration _config;
        public ConfigData1()
        {
            try
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
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

