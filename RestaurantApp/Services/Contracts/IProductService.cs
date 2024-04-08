using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService{
        public Task<IEnumerable<Product>> GetAll(bool trackChanges);
        public Task<IEnumerable<Product>> GetShowcaseProducts(bool trackChanges);
        Task<Product?> GetOne(int id, bool trackChanges);
        public void CreateOne(ProductDtoForInsertion productDto);
        public Task DeleteOne(int id);
        ProductDtoForUpdate GetOneForUpdate(int id, bool trackChanges);
        public void UpdateOne(ProductDtoForUpdate productDto);
    }
}