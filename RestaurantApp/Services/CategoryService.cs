using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<IEnumerable<Category>> GetAll(bool trackChanges){
            return await _manager.Category.GetAllCategories(trackChanges);
        }
    }
}