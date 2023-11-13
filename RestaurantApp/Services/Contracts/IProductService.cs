using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService{
        public IEnumerable<Product> GetAll(bool trackChanges);
        public IEnumerable<Product> GetShowcaseProducts(bool trackChanges);
        Product? GetOne(int id, bool trackChanges);
        public void CreateOne(ProductDtoForInsertion productDto);
        public void DeleteOne(int id);
        ProductDtoForUpdate GetOneForUpdate(int id, bool trackChanges);
        public void UpdateOne(ProductDtoForUpdate productDto);
    }
}