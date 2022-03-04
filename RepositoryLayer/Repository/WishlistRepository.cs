namespace RepositoryLayer.Repository
{
    using Models;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class WishlistRepository : IWishlistRepository
    {
        private readonly IMongoCollection<WishlistModel> Wishlist;

        public WishlistRepository(IDatabaseSetting DB)
        {
            var client = new MongoClient(DB.ConnectionString);
            var Db = client.GetDatabase(DB.DatabaseName);
            Wishlist = Db.GetCollection<WishlistModel>("Wishlist");
        }

        public async Task<WishlistModel> AddToWishlist(WishlistModel addWish)
        {
            try
            {
                var check = await this.Wishlist.Find(x => x.WishlistID == addWish.WishlistID).FirstOrDefaultAsync();
                if (check == null)
                {
                    await this.Wishlist.InsertOneAsync(addWish);
                    return addWish;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RemoveWishlist(WishlistModel delete)
        {
            try
            {
                await this.Wishlist.FindOneAndDeleteAsync(x => x.WishlistID == delete.WishlistID);
                return true;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WishlistModel> GetWishlist()
        {
            try
            {
                return Wishlist.Find(FilterDefinition<WishlistModel>.Empty).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
