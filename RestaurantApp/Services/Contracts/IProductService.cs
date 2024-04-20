using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService{
        //Eski
        //Task<IEnumerable<Product>> GetAll(bool trackChanges);
        //Task<IEnumerable<Product>> GetShowcaseProducts(bool trackChanges);
        //Task<Product?> GetOne(int id, bool trackChanges);
        //void CreateOne(ProductDtoForInsertion productDto);
        //Task DeleteOne(int id);
        //ProductDtoForUpdate GetOneForUpdate(int id, bool trackChanges);
        //void UpdateOne(ProductDtoForUpdate productDto);
        Task<IEnumerable<Product>> GetAll(bool trackChanges);
        Task<IEnumerable<Product>> GetShowcaseProducts(bool trackChanges);
        Task<Product> GetOne(int id, bool trackChanges);
        Task CreateOne(ProductDtoForInsertion productDto);
        Task DeleteOne(int id);
        Task<ProductDtoForUpdate> GetOneForUpdate(int id, bool trackChanges);
        Task UpdateOne(ProductDtoForUpdate productDto);
    }
        
}