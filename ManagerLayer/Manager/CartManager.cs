using ManagerLayer.Interface;
using Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository repo;

        public CartManager(ICartRepository repo)
        {
            this.repo = repo;
        }

        public async Task<CartModel> AddCart(CartModel addCart)
        {
            try
            {
                return await this.repo.AddCart(addCart);
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
                return await this.repo.RemovefromCart(delete);
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
                return await this.UpdateCartQuantity(quantity);
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
                return this.repo.GetCart();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
