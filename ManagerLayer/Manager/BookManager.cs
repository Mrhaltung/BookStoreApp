namespace ManagerLayer.Manager
{
    using ManagerLayer.Interface;
    using Microsoft.AspNetCore.Http;
    using Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class BookManager : IBookManager
    {
        private readonly IBookRepository repo;
        public BookManager(IBookRepository repo)
        {
            this.repo = repo;
        }
        public async Task<BooksModel> AddBook(BooksModel addBook)
        {
            try
            {
                return await this.repo.AddBook(addBook);
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
                return await this.repo.UpdateBook(editBook);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BooksModel> DeleteBook(string BookId)
        {
            try
            {
                return await this.repo.DeleteBook(BookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<BooksModel> GetAllBook()
        {
            try
            {
                return this.repo.GetAllBook();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BooksModel GetbyBookId(string id)
        {
            try
            {
                return this.repo.GetbyBookId(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BooksModel> BookImage(string BookId, IFormFile img)
        {
            try
            {
                return await this.repo.BookImage(BookId, img);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
