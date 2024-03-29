﻿using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RepositoryLayer.Interfaces;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace RepositoryLayer.Sessions
{
    public class BookRepo : IBookRepo
    {
        private readonly IConfiguration _config;

        public BookRepo(IConfiguration config)
        {
            _config = config;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spAddBook", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookTitle", bookModel.BookTitle);
                cmd.Parameters.AddWithValue("@BookAuthor", bookModel.BookAuthor);
                cmd.Parameters.AddWithValue("@BookRating", bookModel.BookRating);
                cmd.Parameters.AddWithValue("@NoOfUsersRated", bookModel.NoOfUsersRated);
                cmd.Parameters.AddWithValue("@BookOriginalPrice", bookModel.BookOriginalPrice);
                cmd.Parameters.AddWithValue("@BookDiscountPrice", bookModel.BookDiscountPrice);
                cmd.Parameters.AddWithValue("@BookDetail", bookModel.BookDetail);
                cmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                cmd.Parameters.AddWithValue("@StockQuantity", bookModel.StockQuantity);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return bookModel;
        }

        public List<BookWithIdModel> GetBooks()
        {
            List<BookWithIdModel> books = new List<BookWithIdModel>();

            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetBooks", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    BookWithIdModel bookModel = new BookWithIdModel();
                    bookModel.BookId = Convert.ToInt32(Reader["BookId"]);
                    bookModel.BookTitle = Reader["BookTitle"].ToString();
                    bookModel.BookAuthor = Reader["BookAuthor"].ToString();
                    bookModel.BookRating = Convert.ToDouble(Reader["BookRating"]);
                    bookModel.NoOfUsersRated = Convert.ToInt32(Reader["NoOfUsersRated"]);
                    bookModel.BookOriginalPrice = Convert.ToInt32(Reader["BookOriginalPrice"]);
                    bookModel.BookDiscountPrice = Convert.ToInt32(Reader["BookDiscountPrice"]);
                    bookModel.BookDetail = Reader["BookDetail"].ToString();
                    bookModel.BookImage = Reader["BookImage"].ToString();
                    bookModel.StockQuantity = Convert.ToInt32(Reader["StockQuantity"]);
                    books.Add(bookModel);
                }
            }
            return books;
        }

        public BookModel GetBook(int id)
        {
            BookModel bookModel = new BookModel();
            int BookId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", id);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    BookId = Convert.ToInt32(Reader["BookId"]);
                    bookModel.BookTitle = Reader["BookTitle"].ToString();
                    bookModel.BookAuthor = Reader["BookAuthor"].ToString();
                    bookModel.BookRating = Convert.ToDouble(Reader["BookRating"]);
                    bookModel.NoOfUsersRated = Convert.ToInt32(Reader["NoOfUsersRated"]);
                    bookModel.BookOriginalPrice = Convert.ToInt32(Reader["BookOriginalPrice"]);
                    bookModel.BookDiscountPrice = Convert.ToInt32(Reader["BookDiscountPrice"]);
                    bookModel.BookDetail = Reader["BookDetail"].ToString();
                    bookModel.BookImage = Reader["BookImage"].ToString();
                    bookModel.StockQuantity = Convert.ToInt32(Reader["StockQuantity"]);
                }
            }
            if(BookId == id)
            {
                return bookModel;
            }
            else
            {
                return null;
            }
        }

        public BookModel EditBook(int id, BookModel bookModel)
        {
            int BookId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", id);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    BookId = Convert.ToInt32(Reader["BookId"]);
                }
                con.Close();
                if (BookId == id)
                {
                    SqlCommand cmdUpdate = new SqlCommand("spEditBook", con);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@BookId", BookId);
                    cmdUpdate.Parameters.AddWithValue("@BookTitle", bookModel.BookTitle);
                    cmdUpdate.Parameters.AddWithValue("@BookAuthor", bookModel.BookAuthor);
                    cmdUpdate.Parameters.AddWithValue("@StockQuantity", bookModel.StockQuantity);
                    cmdUpdate.Parameters.AddWithValue("@BookRating", bookModel.BookRating);
                    cmdUpdate.Parameters.AddWithValue("@NoOfUsersRated", bookModel.NoOfUsersRated);
                    cmdUpdate.Parameters.AddWithValue("@BookOriginalPrice", bookModel.BookOriginalPrice);
                    cmdUpdate.Parameters.AddWithValue("@BookDiscountPrice", bookModel.BookDiscountPrice);
                    cmdUpdate.Parameters.AddWithValue("@BookDetail", bookModel.BookDetail);
                    cmdUpdate.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    cmdUpdate.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    con.Open();
                    cmdUpdate.ExecuteNonQuery();

                    BookModel book = new BookModel();
                    SqlCommand cmdGet = new SqlCommand("spGetBook", con);
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.Parameters.AddWithValue("@BookId", id);

                    SqlDataReader ReaderData = cmd.ExecuteReader();
                    while (ReaderData.Read())
                    {
                        book.BookTitle = ReaderData["BookTitle"].ToString();
                        book.BookAuthor = ReaderData["BookAuthor"].ToString();
                        book.BookRating = Convert.ToDouble(ReaderData["BookRating"]);
                        book.NoOfUsersRated = Convert.ToInt32(ReaderData["NoOfUsersRated"]);
                        book.BookOriginalPrice = Convert.ToInt32(ReaderData["BookOriginalPrice"]);
                        book.BookDiscountPrice = Convert.ToInt32(ReaderData["BookDiscountPrice"]);
                        book.BookDetail = ReaderData["BookDetail"].ToString();
                        book.BookImage = ReaderData["BookImage"].ToString();
                        book.StockQuantity = Convert.ToInt32(ReaderData["StockQuantity"]);
                    }
                    return book;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DeleteBook(int id)
        {
            int BookId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", id);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    BookId = Convert.ToInt32(Reader["BookId"]);
                }
                if (BookId == id)
                {
                    SqlCommand cmdDelete = new SqlCommand("spDeleteBook", con);
                    cmdDelete.CommandType = CommandType.StoredProcedure;
                    cmdDelete.Parameters.AddWithValue("@Id", id);
                    cmdDelete.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<BookModel> FindBook(string name)
        {
            List<BookModel> books = new List<BookModel>();

            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetBookByAuthor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Author", name);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    BookModel bookModel = new BookModel();
                    bookModel.BookTitle = Reader["BookTitle"].ToString();
                    bookModel.BookAuthor = Reader["BookAuthor"].ToString();
                    bookModel.BookRating = Convert.ToDouble(Reader["BookRating"]);
                    bookModel.NoOfUsersRated = Convert.ToInt32(Reader["NoOfUsersRated"]);
                    bookModel.BookOriginalPrice = Convert.ToInt32(Reader["BookOriginalPrice"]);
                    bookModel.BookDiscountPrice = Convert.ToInt32(Reader["BookDiscountPrice"]);
                    bookModel.BookDetail = Reader["BookDetail"].ToString();
                    bookModel.BookImage = Reader["BookImage"].ToString();
                    bookModel.StockQuantity = Convert.ToInt32(Reader["StockQuantity"]);
                    books.Add(bookModel);
                }
            }
            return books;
        }

        public BookModel InsertOrUpdate(int Id,BookModel bookModel)
        {
            int BookId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", Id);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    BookId = Convert.ToInt32(Reader["BookId"]);
                }
                if (BookId == Id)
                {
                    SqlCommand cmdUpdate = new SqlCommand("spEditBook", con);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@BookId", BookId);
                    cmdUpdate.Parameters.AddWithValue("@BookTitle", bookModel.BookTitle);
                    cmdUpdate.Parameters.AddWithValue("@BookAuthor", bookModel.BookAuthor);
                    cmdUpdate.Parameters.AddWithValue("@BookRating", bookModel.BookRating);
                    cmdUpdate.Parameters.AddWithValue("@NoOfUsersRated", bookModel.NoOfUsersRated);
                    cmdUpdate.Parameters.AddWithValue("@BookOriginalPrice", bookModel.BookOriginalPrice);
                    cmdUpdate.Parameters.AddWithValue("@BookDiscountPrice", bookModel.BookDiscountPrice);
                    cmdUpdate.Parameters.AddWithValue("@BookDetail", bookModel.BookDetail);
                    cmdUpdate.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    cmdUpdate.Parameters.AddWithValue("@StockQuantity", bookModel.StockQuantity);
                    cmdUpdate.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmdUpdate.ExecuteNonQuery();
                    return bookModel;
                }
                else
                {
                    SqlCommand cmdInsert = new SqlCommand("spAddBook", con);
                    cmdInsert.CommandType = CommandType.StoredProcedure;

                    cmdInsert.Parameters.AddWithValue("@BookTitle", bookModel.BookTitle);
                    cmdInsert.Parameters.AddWithValue("@BookAuthor", bookModel.BookAuthor);
                    cmdInsert.Parameters.AddWithValue("@BookRating", bookModel.BookRating);
                    cmdInsert.Parameters.AddWithValue("@NoOfUsersRated", bookModel.NoOfUsersRated);
                    cmdInsert.Parameters.AddWithValue("@BookOriginalPrice", bookModel.BookOriginalPrice);
                    cmdInsert.Parameters.AddWithValue("@BookDiscountPrice", bookModel.BookDiscountPrice);
                    cmdInsert.Parameters.AddWithValue("@BookDetail", bookModel.BookDetail);
                    cmdInsert.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    cmdInsert.Parameters.AddWithValue("@StockQuantity", bookModel.StockQuantity);
                    cmdInsert.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmdInsert.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmdInsert.ExecuteNonQuery();
                    return bookModel;
                }
            }
            
        }
    }
}
