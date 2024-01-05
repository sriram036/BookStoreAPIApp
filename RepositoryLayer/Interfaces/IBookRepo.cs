using ModelLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRepo
    {
        BookModel AddBook(BookModel bookModel);
        BookModel GetBook(int id);
        BookModel EditBook(int id, BookModel bookModel);
        bool DeleteBook(int id);
    }
}