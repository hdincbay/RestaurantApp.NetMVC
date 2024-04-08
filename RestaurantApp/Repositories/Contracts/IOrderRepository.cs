using Entities.Models;

namespace Repositories.Contracts
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        Task<Order?> GetOneOrder(int id);
        Task Complete(int id);
        void SaveOrder(Order order);
        int NumberOfInProcess { get; }
    }
}