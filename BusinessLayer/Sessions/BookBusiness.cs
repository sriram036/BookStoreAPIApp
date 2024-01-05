using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepo bookRepo;

        public BookBusiness(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            return bookRepo.AddBook(bookModel);
        }

        public BookModel GetBook(int id)
        {
            return bookRepo.GetBook(id);
        }

        public BookModel EditBook(int id, BookModel bookModel)
        {
            return bookRepo.EditBook(id, bookModel);
        }

        public bool DeleteBook(int id)
        {
            return bookRepo.DeleteBook(id);
        }
    }
}
