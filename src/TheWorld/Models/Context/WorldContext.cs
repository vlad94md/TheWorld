using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Models.Context
{
    public class WorldContext : IdentityDbContext<WorldUser>
    {
        private IConfigurationRoot config;

        public WorldContext(DbContextOptions options, IConfigurationRoot config) : base (options)
        {
            this.config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["ConnectionStrings:WorldContextConnection"]);
        }
    }
}
