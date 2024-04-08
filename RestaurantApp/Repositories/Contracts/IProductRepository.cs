using Entities.Models;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>{
        public Task<IQueryable<Product>> GetAllProducts(bool trackChanges);
        public Task<IQueryable<Product>> GetShowcaseProducts(bool trackChanges);
        Task<Product?> GetOneProduct(int id, bool trackChanges);
        public Task CreateOneProduct(Product product);
        public Task DeleteOneProduct(Product product);
        Task UpdateOneProduct(Product entity);
    }
}