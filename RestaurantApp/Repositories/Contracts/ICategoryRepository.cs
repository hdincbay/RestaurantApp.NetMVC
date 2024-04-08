using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>{
        public Task<IQueryable<Category>> GetAllCategories(bool trackChanges);
    }
}