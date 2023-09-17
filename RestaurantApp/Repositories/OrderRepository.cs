using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Order> Orders => throw new NotImplementedException();

        public int NumberOfInProcess => throw new NotImplementedException();

        public void Complete(int id)
        {
            throw new NotImplementedException();
        }

        public Order? GetOneOrder(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}