using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryService{
        public Task<IEnumerable<Category>> GetAll(bool trackChanges);
    }
}