namespace ManagerLayer.Manager
{
    using ManagerLayer.Interface;
    using Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class WishlistManager : IWishlistManager
    {
        private readonly IWishlistRepository repo;
        public WishlistManager(IWishlistRepository repo)
        {
            this.repo = repo;
        }

        public async Task<WishlistModel> AddToWishlist(WishlistModel addWish)
        {
            try
            {
                return await this.repo.AddToWishlist(addWish);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RemoveWishlist(WishlistModel delete)
        {
            try
            {
                return await this.repo.RemoveWishlist(delete);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WishlistModel> GetWishlist()
        {
            try
            {
                return this.repo.GetWishlist();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
