
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
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .Property(x => x.Timestamp)
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Post>()
                .Property<DateTime>(x => x.createdAt)
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Vote>()
                .HasNoKey();
        }
    }
}
