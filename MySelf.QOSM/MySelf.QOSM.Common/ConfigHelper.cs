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
        /// <summary>
        /// 按类访问，一次访问生成所有的键值对 属性必须和key一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSectionClassValue<T>(string key) where T : new()
        {
            var config = _configuration.GetSection(key).Get<T>();
            return config;
        }
        /// <summary>
        /// 包括全部的路径的额或者单个路径的  路径 a:b  key
        /// </summary>
        /// <param name="path">a:b:c</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetSectionKeyValue(string path, string key)
        {
            return _configuration.GetSection(path)[key];
        }
    }
}

