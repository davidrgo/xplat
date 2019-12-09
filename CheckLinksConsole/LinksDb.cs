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
            var connection = @"Server=localhost;Database=Links;User Id=sa;Password: whatever12!";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
