namespace RepositoryLayer.Interface
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWishlistRepository
    {
        Task<WishlistModel> AddToWishlist(WishlistModel addWish);
        Task<bool> RemoveWishlist(WishlistModel delete);
        IEnumerable<WishlistModel> GetWishlist();

    }
}
