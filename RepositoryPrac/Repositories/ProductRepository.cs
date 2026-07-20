using Microsoft.EntityFrameworkCore;
using RepositoryPrac.Data;
using RepositoryPrac.Model;

namespace RepositoryPrac.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;
        public ProductRepository(ProductDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync<Product>();
        }
    }
}
