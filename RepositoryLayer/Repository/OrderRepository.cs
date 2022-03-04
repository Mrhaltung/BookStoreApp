namespace RepositoryLayer.Repository
{
    using Models;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<OrderModel> Order;

        public OrderRepository(IDatabaseSetting DB)
        {
            var client = new MongoClient(DB.ConnectionString);
            var Db = client.GetDatabase(DB.DatabaseName);
            Order = Db.GetCollection<OrderModel>("Order");
        }

        public async Task<OrderModel> AddOrder(OrderModel addOrder)
        {
            try
            {
                var check = await this.Order.Find(x => x.OrderID == addOrder.OrderID).FirstOrDefaultAsync();
                if (check == null)
                {
                    await this.Order.InsertOneAsync(addOrder);
                    return addOrder;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CancelOrder(OrderModel delete)
        {
            try
            {
                await this.Order.FindOneAndDeleteAsync(x => x.OrderID == delete.OrderID);
                return true;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetOrder()
        {
            try
            {
                return Order.Find(FilterDefinition<OrderModel>.Empty).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
