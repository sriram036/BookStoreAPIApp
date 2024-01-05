using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RepositoryLayer.Interfaces;
using System.Net;
using System.Drawing;

namespace RepositoryLayer.Sessions
{
    public class CartRepo : ICartRepo
    {
        private readonly IConfiguration _config;

        public CartRepo(IConfiguration config)
        {
            _config = config;
        }

        public bool AddToCart(int UserId, CartModel cartModel)
        {
            int BookId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    BookId = Convert.ToInt32(Reader["BookId"]);
                }
            }
            if(BookId == cartModel.BookId)
            {
                using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
                {
                    SqlCommand cmd = new SqlCommand("spAddToCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    cmd.Parameters.AddWithValue("@Quantity", cartModel.Quantity);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
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

        public List<CartModel> GetCarts(int id)
        {
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                List<CartModel> carts = new List<CartModel>();
                SqlCommand cmd = new SqlCommand("spGetCart", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    CartModel cart = new CartModel();
                    cart.BookId = Convert.ToInt32(Reader["BookId"]);
                    cart.Quantity = Convert.ToInt32(Reader["Quantity"]);
                    carts.Add(cart);
                }
                con.Close();

                if(carts.Count == 0)
                {
                    return null;
                }
                else
                {
                    return carts;
                }
            }
        }

        public bool UpdateCart(int count, int cartId, int UserId)
        {
            int UId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetCartByCartId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", cartId);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    UId = Convert.ToInt32(Reader["UserId"]);
                }
                if(UId == UserId)
                {
                    SqlCommand cmdUpdate = new SqlCommand("spUpdateCart", con);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;

                    cmdUpdate.Parameters.AddWithValue("@CartId", cartId);
                    cmdUpdate.Parameters.AddWithValue("@Quantity", count);
                    cmdUpdate.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteCart(int cartId, int UserId)
        {
            int UId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetCartByCartId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", cartId);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    UId = Convert.ToInt32(Reader["UserId"]);
                }
                if (UId == UserId)
                {
                    SqlCommand cmdDelete = new SqlCommand("spDeleteCart", con);
                    cmdDelete.CommandType = CommandType.StoredProcedure;

                    cmdDelete.Parameters.AddWithValue("@Id", cartId);
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
