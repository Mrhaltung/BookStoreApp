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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager manager;

        [HttpPost]
        [Route("AddFeedback")]
        public async Task<IActionResult> AddFeedback(FeedbackModel feedback)
        {
            try
            {
                var res = await this.manager.AddFeedback(feedback);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<FeedbackModel> { Status = true, Message = "Feedback Added", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Feedback not Added" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("Feedback")]
        public IActionResult GetFeedback()
        {
            try
            {
                IEnumerable<FeedbackModel> res = this.manager.GetFeedback();
                if (res != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback Retrived Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Feedback is Empty" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
