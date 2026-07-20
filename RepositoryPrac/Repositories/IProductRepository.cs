using RepositoryPrac.Model;

namespace RepositoryPrac.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts(); //abstract method
    }
}
