using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Sessions
{
    public class OrderRepo : IOrderRepo
    {
        private readonly IConfiguration configuration;

        public OrderRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddOrder(int UserId, OrderModel orderModel)
        {
            CartModel cart = new CartModel();
            int Id = 0;
            int Quantity = 0;
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetCartByCartId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", orderModel.CartId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cart.BookId = Convert.ToInt32(Reader["BookId"]);
                    Id = Convert.ToInt32(Reader["UserId"]);
                }
                if (Id == UserId)
                {
                    SqlCommand cmdGet = new SqlCommand("spGetBook", con);
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.Parameters.AddWithValue("@BookId", cart.BookId);

                    SqlDataReader ReaderGet = cmdGet.ExecuteReader();
                    while (ReaderGet.Read())
                    {
                        Quantity = Convert.ToInt32(ReaderGet["StockQuantity"]);
                    }

                    if (orderModel.Quantity <= Quantity)
                    {
                        SqlCommand cmdAdd = new SqlCommand("spAddOrder", con);
                        cmdAdd.CommandType = CommandType.StoredProcedure;
                        cmdAdd.Parameters.AddWithValue("@UserId", UserId);
                        cmdAdd.Parameters.AddWithValue("@CartId", orderModel.CartId);
                        cmdAdd.Parameters.AddWithValue("@TotalPrice", orderModel.TotalPrice);
                        cmdAdd.Parameters.AddWithValue("@Quantity", orderModel.Quantity);
                        cmdAdd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmdAdd.Parameters.AddWithValue("@BookId", cart.BookId);

                        cmdAdd.ExecuteNonQuery();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public List<OrderModel> GetOrderList(int UserId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                List<OrderModel> ordes = new List<OrderModel>();
                SqlCommand cmd = new SqlCommand("spGetOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    OrderModel order = new OrderModel();
                    order.CartId = Convert.ToInt32(Reader["CartId"]);
                    order.TotalPrice = Convert.ToInt32(Reader["TotalPrice"]);
                    order.Quantity = Convert.ToInt32(Reader["Quantity"]);
                    ordes.Add(order);
                }
                con.Close();

                if (ordes.Count == 0)
                {
                    return null;
                }
                else
                {
                    return ordes;
                }
            }
        }

        public bool DeleteOrder(int UserId, int OrderId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                int User = 0;
                SqlCommand cmd = new SqlCommand("spGetOrderById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    User = Convert.ToInt32(Reader["UserId"]);
                }

                if (UserId == User)
                {
                    SqlCommand cmdDelete = new SqlCommand("spDeleteOrder", con);
                    cmdDelete.CommandType = CommandType.StoredProcedure;

                    cmdDelete.Parameters.AddWithValue("@OrderId", OrderId);

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
