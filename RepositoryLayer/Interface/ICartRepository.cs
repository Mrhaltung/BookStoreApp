﻿namespace RepositoryLayer.Interface
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICartRepository
    {
        Task<CartModel> AddCart(CartModel addCart);
        Task<CartModel> UpdateCartQuantity(CartModel quantity);
        Task<bool> RemovefromCart(CartModel delete);
        IEnumerable<CartModel> GetCart();
    }
}