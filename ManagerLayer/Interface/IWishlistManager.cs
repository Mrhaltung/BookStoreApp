namespace ManagerLayer.Interface
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWishlistManager
    {
        Task<WishlistModel> AddToWishlist(WishlistModel addWish);
        Task<bool> RemoveWishlist(WishlistModel delete);
        IEnumerable<WishlistModel> GetWishlist();
    }
}
