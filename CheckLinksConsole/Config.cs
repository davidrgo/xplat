
using System;
using System.Collections.Generic;
using System.Linq;


namespace CheckLinksConsole
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class Config
    {
        public Config(string[] args)
        {
            var inMemory = new Dictionary<string, string>
            {
                { "site","http://www.coyotebroad.com/coyotehelps/index.html" },
                {"output:folder" , "reports"},
                {"output:file" , "report.txt"},
            };
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("checksettings.json", true)
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                ;
            var configuration = configBuilder.Build();
            ConfigurationRoot = configuration;
            Site = configuration["site"];
            var section = configuration.GetSection("output");
            Output = configuration.GetSection("output").Get<OutputSettings>();
        }

        public string Site { get; set; }
        public OutputSettings Output { get; set; }
        public IConfigurationRoot ConfigurationRoot { get; set; }
    }
}