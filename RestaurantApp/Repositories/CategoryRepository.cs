using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Category> GetAllCategories(bool trackChanges) => FindAll(trackChanges);
    }
}