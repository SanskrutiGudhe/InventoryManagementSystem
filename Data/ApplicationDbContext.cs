using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set restrict deletes to bypass Cascade Delete cycles in SQL Server
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sales>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
