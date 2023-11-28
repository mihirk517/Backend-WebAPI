
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics.Metrics;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=SuperHerodb;Trusted_Connection=True;TrustServerCertificate=true;");
        }
        public DbSet<SuperHero> SuperHeroes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
