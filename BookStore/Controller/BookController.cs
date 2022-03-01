namespace BookStore.Controller
{
    using ManagerLayer.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;

        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddBook(BooksModel addBook)
        {
            try
            {
                var res = await this.manager.AddBook(addBook);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<BooksModel> { Status = true, Message = "Book Added Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Added" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("EditBook")]
        public async Task<IActionResult> UpdateBook(BooksModel editBook)
        {
            try
            {
                var res = await this.manager.UpdateBook(editBook);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<BooksModel> { Status = true, Message = "Book Updated Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Updated" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(string BookId)
        {
            try
            {
                var res = await this.manager.DeleteBook(BookId);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<BooksModel> { Status = true, Message = "Book Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Deleted" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBook()
        {
            try
            {
                IEnumerable<BooksModel> res = this.manager.GetAllBook();
                if (res != null)
                {
                    return this.Ok(new { Status = true, Message = "Books Retrived Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Books not Found" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetbyBookId")]
        public IActionResult GetByBookId(string id)
        {
            try
            {
                var res = this.manager.GetbyBookId(id);
                if (res != null)
                {
                    return this.Ok(new { Status = true, Message = "Book Found Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Found" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("UploadImage")]
        public async Task<IActionResult> BookImg(string BookId, IFormFile img)
        {
            try
            {
                var res = await this.manager.BookImage(BookId, img);
                if (res != null)
                {
                    return this.Ok(new ResponseModel<BooksModel> { Status = true, Message = "Image Uploaded Successfully", Data = res });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Image not Uploaded" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
