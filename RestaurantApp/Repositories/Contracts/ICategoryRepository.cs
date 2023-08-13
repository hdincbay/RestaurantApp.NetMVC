using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>{
        public IQueryable<Category> GetAllCategories(bool trackChanges);
    }
}