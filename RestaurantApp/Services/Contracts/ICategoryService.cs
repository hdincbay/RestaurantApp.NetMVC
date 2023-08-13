using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryService{
        public IEnumerable<Category> GetAll(bool trackChanges);
    }
}