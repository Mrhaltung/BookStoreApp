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
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;

        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddAddress")]
        public async Task<IActionResult> AddAddress(AddressModel addAddress)
        {
            try
            {
                var res = await this.manager.AddAddress(addAddress);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Added Successfully", Data = res });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Added" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(AddressModel editAddress)
        {
            try
            {
                var res = await this.manager.AddAddress(editAddress);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Updated Successfully", Data = res });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Updated" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(AddressModel delete)
        {
            try
            {
                var res = await this.manager.DeleteAddress(delete);
                if (res == true)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Deleted Successfully" });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Deleted" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetAllAddress")]
        public IActionResult GetallAddress()
        {
            try
            {
                IEnumerable<AddressModel> res = this.manager.GetAllAddress();
                if (res != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Retrived Successfully", Data = res });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Retrived" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetByAddressType")]
        public async Task<IActionResult> GetByAddressType(string addTypeId)
        {
            try
            {
                var res = await this.manager.GetByAddressType(addTypeId);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Retrived Successfully", Data = res });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Retrived" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }
    }
}
