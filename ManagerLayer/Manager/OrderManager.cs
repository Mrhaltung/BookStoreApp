namespace ManagerLayer.Manager
{
    using ManagerLayer.Interface;
    using Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository repo;
        public OrderManager(IOrderRepository repo)
        {
            this.repo = repo;
        }

        public async Task<OrderModel> AddOrder(OrderModel addOrder)
        {
            try
            {
                return await this.repo.AddOrder(addOrder);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CancelOrder(OrderModel delete)
        {
            try
            {
                return await this.repo.CancelOrder(delete);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetOrder()
        {
            try
            {
                return this.repo.GetOrder();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
