using Entities.Models;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>{
        public IQueryable<Product> GetAllProducts(bool trackChanges);
        Product? GetOneProduct(int id, bool trackChanges);
        public void CreateOneProduct(Product product);
        public void DeleteOneProduct(Product product);
        void UpdateOneProduct(Product entity);
    }
}