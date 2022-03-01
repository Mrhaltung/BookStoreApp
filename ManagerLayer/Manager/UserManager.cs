namespace ManagerLayer.Manager
{
    using ManagerLayer.Interface;
    using Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class UserManager : IUserManager
    {
        private readonly IUserRepository repo;

        public UserManager(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<bool> Forget(string email)
        {
            try
            {
                return await this.repo.Forget(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RegisterModel> Login(LoginModel login)
        {
            login.Password = EncodePasswordToBase64(login.Password);
            try
            {
                return await this.repo.Login(login);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            register.Password = EncodePasswordToBase64(register.Password);
            try
            {
                return await this.repo.Register(register);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RegisterModel> Reset(ResetModel reset)
        {
            reset.NewPassword = EncodePasswordToBase64(reset.NewPassword);
            try
            {
                return await this.repo.Reset(reset);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GenerateToken(string email)
        {
            try
            {
                return this.repo.GenerateToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
