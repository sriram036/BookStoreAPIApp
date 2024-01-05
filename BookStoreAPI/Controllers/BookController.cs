using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddUser(BookModel bookModel)
        {
            BookModel book = bookBusiness.AddBook(bookModel);

            if(book == null)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Book Not Added", Data = "User Not Found" });
            }
            else
            {
                return Ok(new ResponseModel<BookModel> { IsSuccess = true, Message = "Book Added", Data = book });
            }
        }

        [HttpGet]
        [Route("GetBook")]
        public IActionResult GetBook(int id)
        {
            BookModel bookModel = bookBusiness.GetBook(id);

            if(bookModel == null)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Book Not Found", Data = "Book Id is not Available" });
            }
            else
            {
                return Ok(new ResponseModel<BookModel> { IsSuccess = true, Message = "Book Found", Data = bookModel });
            }
        }

        [HttpPut]
        [Route("EditBook")]
        public IActionResult EditBook(int id, BookModel bookModel)
        {
            BookModel book = bookBusiness.EditBook(id, bookModel);

            if( book == null)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Book Not Updated", Data = "Book Id is not in the List" });
            }
            else
            {
                return Ok(new ResponseModel<BookModel> { IsSuccess = true, Message = "Book Updated", Data = book });
            }
        }

        [HttpDelete]
        [Route("RemoveBook")]
        public IActionResult DeleteBook(int id)
        {
            bool IsDeleted = bookBusiness.DeleteBook(id);

            if (IsDeleted)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Book Deleted Successfully", Data = "Book Id Found and Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Book Not Deleted", Data = "Book Id Not Found or Matched" });
            }
        }
    }
}
