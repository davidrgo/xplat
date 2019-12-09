using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLinksConsole
{
    public static class Logs
    {
        public static LoggerFactory Factory = new LoggerFactory();
        public static void Init(IConfiguration configuration)
        {
            //Factory.AddConsole(LogLevel.Trace, includeScopes: true);
            Factory.AddConsole(configuration.GetSection("Logging"));
            Factory.AddFile("logs/checklinks-{Date}.json",
                isJson: true,
                minimumLevel:LogLevel.Trace);
        }
    }
}
