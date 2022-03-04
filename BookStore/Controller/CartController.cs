namespace BookStore.Controller
{
    using ManagerLayer.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;

        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddtoCart")]
        public async Task<IActionResult> AddCart(CartModel addCart)
        {
            try
            {
                var res = await this.manager.AddCart(addCart);
                if(res == null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Added to Cart", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added to Cart" });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateQuantity")]
        public async Task<IActionResult> UpdateCartQuantity(CartModel quantity)
        {
            try
            {
                var res = await this.manager.UpdateCartQuantity(quantity);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Quantity Updated", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Cannot Update Quantity" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveItem")]
        public async Task<IActionResult> RemovefromCart(CartModel delete)
        {
            try
            {
                var res = await this.manager.RemovefromCart(delete);
                if (res != false)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Removed from Cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Removed from Cart" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetCart")]
        public IActionResult GetCart()
        {
            try
            {
                IEnumerable<CartModel> res = this.manager.GetCart();
                if (res != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Retrived Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Cart is Empty" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
