using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLinksConsole
{
    public class LinksDb : DbContext
    {
        public DbSet<LinkCheckResult> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=localhost;Database=Links;User Id=SA;Password=1Secure*Password1";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
