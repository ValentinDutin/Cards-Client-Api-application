using Microsoft.EntityFrameworkCore;
using CommonFiles.Models;

namespace CardsServer.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { Database.EnsureCreated(); }
        public DbSet<Card> Cards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().ToTable("Cards");
            base.OnModelCreating(modelBuilder);
        }
    }
}
