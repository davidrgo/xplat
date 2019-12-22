using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CheckLinksConsole
{
	using Hangfire;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
    
    using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
    using System.Collections.Generic;

    class Program
	{
        
		static void Main(string[] args)
		{

			var config = new Config(args);
            Logs.Init(config.ConfigurationRoot);
            var logger = Logs.Factory.CreateLogger<Program>();
            
            Directory.CreateDirectory(config.Output.GetReportDirectory());
            logger.LogInformation(200,$"Saving report to {config.Output.GetReportDirectory()}");
            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);
            logger.LogDebug(body.Result);

            logger.LogInformation("Links");
            var links = LinkChecker.GetLinks(config.Site,body.Result);
            links.ToList().ForEach(Console.WriteLine);
            var checkedLinks = LinkChecker.CheckLinks(links);
         
            using (var file = File.CreateText(config.Output.GetReportFilePath()))
            using (var linksDb = new LinksDb())
            {
                foreach (var link in checkedLinks.OrderBy(l => l.Exists))
                {
                    
                    var status = link.IsMissing ? "Missing" : "OK";
                    file.WriteLine($"{status} - {link.Link}");
                    linksDb.Links.Add(link);
		            Console.WriteLine("{0} {1}",link.Id, link.Link);
                }

                /**
		 * docker run -d --name sqllinux-netcore -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1Secure*Password1' -e 'MSSQL_PID=Enterprise' -p 1433:1433 -d microsoft/mssql-server-linux:2017-latest
                 * docker hub: https://hub.docker.com/r/microsoft/mssql-server-linux
		 * docker run -d --name mysql-netcore -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=Links -p 3306:3306 mysql
                 */
                linksDb.SaveChanges();
            }
           
		}
	}

	

}
