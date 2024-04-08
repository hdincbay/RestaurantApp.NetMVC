using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateOneProduct(Product product) => await Create(product);

        public async Task DeleteOneProduct(Product product) => await Delete(product);

        public async Task<IQueryable<Product>> GetAllProducts(bool trackChanges) => await FindAll(trackChanges);

        public async Task<Product?> GetOneProduct(int id, bool trackChanges) => await FindByCondition(p => p.ProductId.Equals(id), trackChanges);

        public async Task<IQueryable<Product>> GetShowcaseProducts(bool trackChanges)
        {
            var products = await FindAll(trackChanges);
            return products.Where(r => r.ShowCase.Equals(true));
        }


        public async Task UpdateOneProduct(Product entity) => await Update(entity);
    }
}