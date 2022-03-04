namespace RepositoryLayer.Repository
{
    using Models;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<CartModel> Cart;

        public CartRepository(IDatabaseSetting DB)
        {
            var client = new MongoClient(DB.ConnectionString);
            var Db = client.GetDatabase(DB.DatabaseName);
            Cart = Db.GetCollection<CartModel>("Cart");
        }

        public async Task<CartModel> AddCart(CartModel addCart)
        {
            try
            {
                var check = await this.Cart.Find(x => x.CartID == addCart.CartID).FirstOrDefaultAsync();
                if(check == null)
                {
                    await this.Cart.InsertOneAsync(addCart);
                    return check;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RemovefromCart(CartModel delete)
        {
            try
            {
                await this.Cart.FindOneAndDeleteAsync(x => x.CartID == delete.CartID);
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CartModel> UpdateCartQuantity(CartModel quantity)
        {
            try
            {
                var check = await this.Cart.Find(x => x.CartID == quantity.CartID).FirstOrDefaultAsync();
                if(check != null)
                {
                    await this.Cart.UpdateOneAsync(x => x.CartID == quantity.CartID,
                        Builders<CartModel>.Update.Set(x => x.Quantity, quantity.Quantity));
                    return check;
                }
                return check;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CartModel> GetCart()
        {
            try
            {
                return Cart.Find(FilterDefinition<CartModel>.Empty).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
