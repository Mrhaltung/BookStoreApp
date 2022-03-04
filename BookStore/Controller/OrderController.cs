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
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager manager;

        public OrderController(IOrderManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderModel addOrder)
        {
            try
            {
                var res = await this.manager.AddOrder(addOrder);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = "Order Placed", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order not Placed" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("CancelOrder")]
        public async Task<IActionResult> CancelOrder(OrderModel delete)
        {
            try
            {
                var res = await this.manager.CancelOrder(delete);
                if (res != false)
                {
                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = "Order Canclled" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order not Canclled" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetOrder")]
        public IActionResult GetOrder()
        {
            try
            {
                IEnumerable<OrderModel> res = this.manager.GetOrder();
                if (res != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Retrived Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order is Empty" });
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
