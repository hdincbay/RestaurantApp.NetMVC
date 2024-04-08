using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>{
        public Task<IQueryable<T>> FindAll(bool trackChanges);
        Task<T?> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        public Task Create(T entity);
        public Task Delete(T entity);
        public Task Update(T entity);
    }
}