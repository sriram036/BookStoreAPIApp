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
    public class UserAddressRepo : IUserAddressRepo
    {
        private readonly IConfiguration configuration;

        public UserAddressRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public UserAddressModel AddUserAddress(int UserId, UserAddressModel userAddressModel)
        {
            int AddressType = 0;
            List<int> types = new List<int>();
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserAddressType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    AddressType = Convert.ToInt32(Reader["UserAddressType"]);
                    types.Add(AddressType);
                }
                if (userAddressModel.UserAddressType != types.Find(type => type.Equals(userAddressModel.UserAddressType)) && userAddressModel.UserAddressType > 0 && userAddressModel.UserAddressType < 4)
                {
                    SqlCommand cmdAdd = new SqlCommand("spAddUserAddress", con);

                    cmdAdd.Parameters.AddWithValue("@UserAddressType", userAddressModel.UserAddressType);
                    cmdAdd.Parameters.AddWithValue("@UserId", UserId);
                    cmdAdd.Parameters.AddWithValue("@UserAddress", userAddressModel.UserAddress);
                    cmdAdd.Parameters.AddWithValue("@UserCity", userAddressModel.UserCity);
                    cmdAdd.Parameters.AddWithValue("@UserState", userAddressModel.UserState);
                    cmdAdd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmdAdd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmdAdd.CommandType = CommandType.StoredProcedure;
                    cmdAdd.ExecuteNonQuery();
                    return userAddressModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<UserAddressModel> GetUserAddresses(int UserId)
        {
            List<UserAddressModel> userAddresses = new List<UserAddressModel>();

            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserAddressType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    UserAddressModel userAddressModel = new UserAddressModel();
                    userAddressModel.UserAddress = Reader["UserAddress"].ToString();
                    userAddressModel.UserCity = Reader["UserCity"].ToString();
                    userAddressModel.UserState = Reader["UserState"].ToString();
                    userAddressModel.UserAddressType = Convert.ToInt32(Reader["UserAddressType"]);

                    userAddresses.Add(userAddressModel);
                }
            }
            return userAddresses;
        }

        public bool UpdateUserAddress(int UserId, UserAddressModel userAddressModel)
        {
            int AddressType = 0;
            List<int> types = new List<int>();
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserAddressType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    AddressType = Convert.ToInt32(Reader["UserAddressType"]);
                    types.Add(AddressType);
                }
                if (userAddressModel.UserAddressType == types.Find(type => type.Equals(userAddressModel.UserAddressType)) && userAddressModel.UserAddressType > 0 && userAddressModel.UserAddressType < 4)
                {
                    SqlCommand cmdUpdate = new SqlCommand("spUpdateUserAddress", con);

                    cmdUpdate.Parameters.AddWithValue("@UserAddressType", userAddressModel.UserAddressType);
                    cmdUpdate.Parameters.AddWithValue("@UserId", UserId);
                    cmdUpdate.Parameters.AddWithValue("@UserAddress", userAddressModel.UserAddress);
                    cmdUpdate.Parameters.AddWithValue("@UserCity", userAddressModel.UserCity);
                    cmdUpdate.Parameters.AddWithValue("@UserState", userAddressModel.UserState);
                    cmdUpdate.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteUser(int UserId, int Type)
        {
            int AddressType = 0;
            List<int> types = new List<int>();
            using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserAddressType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    AddressType = Convert.ToInt32(Reader["UserAddressType"]);
                    types.Add(AddressType);
                }
                if (Type == types.Find(type => type.Equals(Type)) && Type > 0 && Type < 4)
                {
                    SqlCommand cmdDelete = new SqlCommand("spDeleteUserAddress", con);

                    cmdDelete.Parameters.AddWithValue("@UserAddressType", Type);
                    cmdDelete.Parameters.AddWithValue("@UserId", UserId);
                    cmdDelete.CommandType = CommandType.StoredProcedure;
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
