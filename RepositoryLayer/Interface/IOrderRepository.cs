namespace RepositoryLayer.Interface
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOrderRepository
    {
        Task<OrderModel> AddOrder(OrderModel addOrder);
        Task<bool> CancelOrder(OrderModel delete);
        IEnumerable<OrderModel> GetOrder();
    }
}
