using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(IProductRepository productRepository, ICategoryRepository categoryRepository, RepositoryContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}