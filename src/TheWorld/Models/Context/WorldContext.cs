using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheWorld.Models.Context
{
    public class WorldContext : DbContext
    {
        public WorldContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\v11.0;Initial Catalog=TheWorldDb;Integrated Security=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
