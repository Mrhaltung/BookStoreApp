namespace RepositoryLayer.Repository
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Models;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<BooksModel> Books;
        private readonly IConfiguration configuration;

        public BookRepository(IDatabaseSetting DB, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(DB.ConnectionString);
            var Db = client.GetDatabase(DB.DatabaseName);
            Books = Db.GetCollection<BooksModel>("Books");
        }

        public async Task<BooksModel> AddBook(BooksModel addBook)
        {
            try
            {
                var ifExist = await this.Books.Find(x => x.BookID == addBook.BookID).FirstOrDefaultAsync();
                if(ifExist == null)
                {
                    await this.Books.InsertOneAsync(addBook);
                    return addBook;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BooksModel> UpdateBook(BooksModel editBook)
        {
            try
            {
                var ifExist = await this.Books.Find(x => x.BookID == editBook.BookID).FirstOrDefaultAsync();
                if (ifExist != null)
                {
                    await this.Books.UpdateOneAsync(x => x.BookID == editBook.BookID,
                        Builders<BooksModel>.Update.Set(x => x.BookName, editBook.BookName)
                        .Set(x => x.Description, editBook.Description)
                        .Set(x => x.AuthorName, editBook.AuthorName)
                        .Set(x => x.Rating, editBook.Rating)
                        .Set(x => x.TotalRating, editBook.TotalRating)
                        .Set(x => x.DiscountPrice, editBook.DiscountPrice));
                    return ifExist;
                }
                else
                {
                    await this.Books.InsertOneAsync(editBook);
                    return editBook;
                }                        
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BooksModel> DeleteBook(string BookId)
        {
            try
            {
                var ifExist = await this.Books.FindOneAndDeleteAsync(x => x.BookID == BookId);
                return ifExist;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BooksModel> BookImage(string BookId, IFormFile img)
        {
            try
            {
                Account account = new Account(this.configuration["CloudinaryAccount:Name"],
                    this.configuration["CloudinaryAccount:API-Key"], 
                    this.configuration["CloudinaryAccount:API-Secret"]);
                var cloudinary = new Cloudinary(account);
                var uploadparams = new ImageUploadParams()
                {
                    File = new FileDescription(img.FileName, img.OpenReadStream()),
                };

                var uploadResult = cloudinary.Upload(uploadparams);
                string imagePath = uploadResult.Url.ToString();
                var ifExist = this.Books.AsQueryable().Where(x => x.BookID == BookId).FirstOrDefault();

                if(ifExist != null)
                {
                    ifExist.BookImage = imagePath;
                    await this.Books.UpdateOneAsync(x => x.BookID == BookId,
                        Builders<BooksModel>.Update.Set(x => x.BookImage, ifExist.BookImage));
                    return ifExist;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<BooksModel> GetAllBook()
        {
            try
            {
                return Books.Find(FilterDefinition<BooksModel>.Empty).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BooksModel GetbyBookId(string id)
        {
            try
            {
                return Books.Find(x => x.BookID == id).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
