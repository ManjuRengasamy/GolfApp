using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GolfApplication.Data
{
    public class Common
    {

        #region DBCon
        public static IConfiguration configuration
        {
            get;
            private set;
        }
        public Common(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public static string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            IConfigurationRoot configuration = builder.Build();
            var connstring = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            return connstring;
        }
        #endregion

    }
}
