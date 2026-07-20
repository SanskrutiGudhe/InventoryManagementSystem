using Microsoft.EntityFrameworkCore;
using RepositoryPrac.Model;

namespace RepositoryPrac.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
         public DbSet<Product> Products { get; set; }
       
    }
}
