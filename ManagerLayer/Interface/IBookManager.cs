namespace ManagerLayer.Interface
{
    using Microsoft.AspNetCore.Http;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBookManager
    {
        Task<BooksModel> AddBook(BooksModel addBook);
        Task<BooksModel> UpdateBook(BooksModel editBook);
        Task<BooksModel> DeleteBook(string BookId);
        Task<BooksModel> BookImage(string BookId, IFormFile img);
        IEnumerable<BooksModel> GetAllBook();
        BooksModel GetbyBookId(string id);
    }
}
