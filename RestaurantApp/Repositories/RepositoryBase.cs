using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            await Task.Run(() => _context.Set<T>().Add(entity));
        }

        public async Task Delete(T entity)
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }

        public async Task<IQueryable<T>> FindAll(bool trackChanges)
        {
            return await Task.Run(() => trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking());
        }

        public async Task<T?> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return await Task.Run(() => trackChanges ? _context.Set<T>().Where(expression).SingleOrDefault() : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault());
        }

        public async Task Update(T entity)
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
        }
    }
}