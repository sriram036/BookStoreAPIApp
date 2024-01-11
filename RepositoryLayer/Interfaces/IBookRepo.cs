using ModelLayer.Models;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRepo
    {
        BookModel AddBook(BookModel bookModel);
        List<BookWithIdModel> GetBooks();
        BookModel GetBook(int id);
        BookModel EditBook(int id, BookModel bookModel);
        bool DeleteBook(int id);
        List<BookModel> FindBook(string name);
        BookModel InsertOrUpdate(int Id, BookModel bookModel);
    }
}