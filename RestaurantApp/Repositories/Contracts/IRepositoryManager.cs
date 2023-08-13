namespace Repositories.Contracts
{
    public interface IRepositoryManager{
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        public void Save();
        
    }
}