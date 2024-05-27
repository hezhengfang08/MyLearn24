using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Common
{
    public static class ConfigHelper
    {
        private static IConfigurationRoot _configuration;
        public static void Init(string path)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path, optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }
        public static T GetAppSettings<T>(string key) where T : new()
        {
            var config = _configuration.GetSection(key).Get<T>();
            return config;
        }
    }
}
