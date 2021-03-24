using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Frameworks
{
    /// <summary>
    /// 约定：固定读取更目录下面的appsettings.json
    /// </summary>
    public class ConfigrationManager
    {
        //有了IOC在去注入--容器单列
        static ConfigrationManager()
        {
            //需要NuGet引入Microsoft.Extensions.Configuration/Microsoft.Extensions.Configuration.FileExtensions/Microsoft.Extensions.Configuration.Json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            _SqlConnectionStringWrite = configuration["ConnectionString:Write"];

            _SqlConnectionStringRead = configuration.GetSection("ConnectionString").GetSection("Read").GetChildren().Select(s => s.Value).ToArray();
        }

        private static string _SqlConnectionStringWrite = null;
        public static string SqlConnectionStringWrite
        {
            get
            {
                return _SqlConnectionStringWrite;
            }
        }

        private static string[] _SqlConnectionStringRead = null;
        public static string[] SqlConnectionStringRead
        {
            get
            {
                return _SqlConnectionStringRead;
            }
        }
    }
}
