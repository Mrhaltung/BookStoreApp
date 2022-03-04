namespace BookStore.Controller
{
    using ManagerLayer.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistManager manager;

        public WishlistController(IWishlistManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddToWishlist")]
        public async Task<IActionResult> AddToWishlist(WishlistModel addWish)
        {
            try
            {
                var res = await this.manager.AddToWishlist(addWish);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<WishlistModel> { Status = true, Message = "Book Added to Wishlist", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added to Wishlist" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveFromWishlist")]
        public async Task<IActionResult> RemoveWishlist(WishlistModel delete)
        {
            try
            {
                var res = await this.manager.RemoveWishlist(delete);
                if (res != false)
                {
                    return this.Ok(new ResponseModel<WishlistModel> { Status = true, Message = "Book Removed from Wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Removed from Wishlist" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetWishlist")]
        public IActionResult GetWishlist()
        {
            try
            {
                IEnumerable<WishlistModel> res = this.manager.GetWishlist();
                if (res != null)
                {
                    return this.Ok(new { Status = true, Message = "Wishlist Retrived Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Wishlist is Empty" });
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
