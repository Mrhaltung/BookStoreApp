namespace RepositoryLayer.Repository
{
    using Experimental.System.Messaging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<RegisterModel> User;
        private readonly IConfiguration configuration;

        public UserRepository(IDatabaseSetting DB, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userClient = new MongoClient(DB.ConnectionString);
            var Db = userClient.GetDatabase(DB.DatabaseName);
            User = Db.GetCollection<RegisterModel>("User");
        }

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.EmailID == register.EmailID).FirstOrDefault();
                if(check == null)
                {
                    await this.User.InsertOneAsync(register);
                    return register;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RegisterModel> Login(LoginModel login)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.EmailID == login.EmailID).FirstOrDefault();
                if(check != null)
                {
                    check = this.User.AsQueryable().Where(x => x.EmailID == login.EmailID).FirstOrDefault();
                    if(check != null)
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "Full Name", check.FullName);
                        database.StringSet(key: "Mobile", check.Mobile);
                        database.StringSet(key: "Email", check.EmailID);
                        database.StringSet(key: "UserID", check.UserID);
                        return check;
                    }
                    return null;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RegisterModel> Reset(ResetModel reset)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.EmailID == reset.EmailID).FirstOrDefault();
                if(check != null)
                {
                    await this.User.UpdateOneAsync(x => x.EmailID == reset.EmailID,
                        Builders<RegisterModel>.Update.Set(x => x.Password, reset.NewPassword));
                    return check;
                }
                return null;   
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                      { new Claim(ClaimTypes.Email, email) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public async Task<bool> Forget(string email)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.EmailID == email).FirstOrDefault();
                if(check != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(email);
                    mail.Subject = "Reset Password for BookStore";
                    this.SendMSMQ();
                    mail.Body = this.ReceiveMSMQ();

                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SendMSMQ()
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\BookStore"))
            {
                msgqueue = new MessageQueue(@".\Private$\BookStore");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\BookStore");
            }

            msgqueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "This is Password reset link. Reset Link => ";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }

        public string ReceiveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\BookStore");
            var receivemessage = msgqueue.Receive();
            receivemessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemessage.Body.ToString();
        }
    }
}
