using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IBookBusiness
    {
        BookModel AddBook(BookModel bookModel);
        BookModel GetBook(int id);
        BookModel EditBook(int id, BookModel bookModel);
        bool DeleteBook(int id);
    }
}