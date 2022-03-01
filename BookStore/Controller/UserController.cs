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
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
                var res = await this.manager.Register(register);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Registered Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "User Not Registered" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                var res = await this.manager.Login(login);
                if (res != null)
                {
                    string token = this.manager.GenerateToken(login.EmailID);
                    return this.Ok(new { Status = true, Message = "Login Successfully", Data = res, Token = token });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Login Unsuccessful" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> Forget(string email)
        {
            try
            {
                var res = await this.manager.Forget(email);
                if (res == true)
                {
                    return this.Ok(new { Status = true, Message = "Link Send Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Link not Sent" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> Reset([FromBody] ResetModel reset)
        {
            try
            {
                var resp = await this.manager.Reset(reset);
                if (resp != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Password Reset Successful", Data = resp });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "User Password not Reset" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
