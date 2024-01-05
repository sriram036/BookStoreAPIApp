using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Sessions
{
    public class WishListRepo : IWishListRepo
    {
        private readonly IConfiguration _config;

        public WishListRepo(IConfiguration config)
        {
            _config = config;
        }

        public bool AddToWishList(int UserId, int Id)
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
            }
            if (BookId == Id)
            {
                using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
                {
                    SqlCommand cmd = new SqlCommand("spAddToWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public List<int> GetWishList(int Id)
        {
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                List<int> Books = new List<int>();
                SqlCommand cmd = new SqlCommand("spGetWishList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", Id);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    int Book = Convert.ToInt32(Reader["BookId"]);
                    Books.Add(Book);
                }
                con.Close();

                if (Books.Count == 0)
                {
                    return null;
                }
                else
                {
                    return Books;
                }
            }
        }

        public bool DeleteWishList(int WishId, int UserId)
        {
            int UId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetWishListByWishId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", WishId);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    UId = Convert.ToInt32(Reader["UserId"]);
                }
                if (UId == UserId)
                {
                    SqlCommand cmdDelete = new SqlCommand("spDeleteWishList", con);
                    cmdDelete.CommandType = CommandType.StoredProcedure;

                    cmdDelete.Parameters.AddWithValue("@WishId", WishId);
                    cmdDelete.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
