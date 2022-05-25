using System;
using System.IO;
using BoDi;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SpecFlowProjectTest.Utils
{
   public class AppSettings
    {
        public IConfiguration _configuration { get; set; }
        public AppSettings()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var settingsFile = Path.Combine(baseDir, "appSettings.json");
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(settingsFile)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
