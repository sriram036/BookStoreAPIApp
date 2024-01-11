using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;
using Newtonsoft.Json.Linq;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration _config;

        public UserRepo(IConfiguration config)
        {
            _config = config;
        }

        public UserModel AddUser(UserModel userModel)
        {
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", userModel.FullName);
                cmd.Parameters.AddWithValue("@EmailId", userModel.Email);
                cmd.Parameters.AddWithValue("@Password", EncodePassword(userModel.Password));
                cmd.Parameters.AddWithValue("@MobileNumber", long.Parse(userModel.MobileNumber));
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return userModel;
        }

        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string LoginUser(LoginModel loginModel)
        {
            string token = "";
            int UserId = 0;
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", loginModel.Email);
                cmd.Parameters.AddWithValue("@Password", EncodePassword(loginModel.Password));

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    UserId = Convert.ToInt32(Reader["UserId"]);
                }

                if(UserId != 0)
                {
                    token = GenerateToken(loginModel.Email, UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        public UserModel getUser(int UserId)
        {
            int User = 0;
            UserModel userModel = new UserModel();
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserByUserId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    User = Convert.ToInt32(Reader["UserId"]);
                    userModel.FullName = Reader["FullName"].ToString();
                    userModel.Email = Reader["EmailId"].ToString();
                    userModel.Password = Reader["Password"].ToString();
                    userModel.MobileNumber = Reader["MobileNumber"].ToString();
                }

                if (User != 0)
                {
                    return userModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public ForgotPasswordModel ForgotPassword(string Email)
        {
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserByEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    forgotPasswordModel.Email = Reader["EmailId"].ToString();
                    forgotPasswordModel.UserId = Convert.ToInt32(Reader["UserId"]);
                }
            }
            if(forgotPasswordModel.Email is null)
            {
                return null;
            }
            else
            {
                string token = GenerateToken(forgotPasswordModel.Email, forgotPasswordModel.UserId);
                forgotPasswordModel.Token = token;
                return forgotPasswordModel;
            }
        }

        public string GenerateToken(string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issue"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            string EmailId="";
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserByEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    EmailId = Reader["EmailId"].ToString();
                }
                if (EmailId == Email)
                {
                    SqlCommand cmdUpdate = new SqlCommand("spUpdatePassword", con);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@Email", Email);
                    cmdUpdate.Parameters.AddWithValue("@Password", EncodePassword(resetPasswordModel.Password));
                    cmdUpdate.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmdUpdate.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public UserModel UpdateUser(string Email, string Name, string Number)
        {
            string EmailId = "";
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserByEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    EmailId = Reader["EmailId"].ToString();
                }
                con.Close();
                if (EmailId == Email)
                {
                    SqlCommand cmdUpdate = new SqlCommand("spUpdateUser", con);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@FullName", Name);
                    cmdUpdate.Parameters.AddWithValue("@MobileNumber", long.Parse(Number));
                    cmdUpdate.Parameters.AddWithValue("@Email", Email);
                    cmdUpdate.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    con.Open();
                    cmdUpdate.ExecuteNonQuery();

                    UserModel userModel = new UserModel();
                    SqlCommand cmdGet = new SqlCommand("spGetUserByEmail", con);
                    cmdGet.CommandType = CommandType.StoredProcedure;
                    cmdGet.Parameters.AddWithValue("@Email", Email);

                    SqlDataReader ReaderData = cmd.ExecuteReader();
                    while (ReaderData.Read())
                    {
                        userModel.FullName = ReaderData["FullName"].ToString();
                        userModel.Email = ReaderData["EmailId"].ToString();
                        userModel.Password = ReaderData["Password"].ToString();
                        userModel.MobileNumber = ReaderData["MobileNumber"].ToString();
                    }
                    return userModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DeleteUser(string Email)
        {
            string EmailId = "";
            using (SqlConnection con = new SqlConnection(_config["ConnectionStrings:BookStoreConnection"]))
            {
                SqlCommand cmd = new SqlCommand("spGetUserByEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);

                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    EmailId = Reader["EmailId"].ToString();
                }
                if (EmailId == Email)
                {
                    SqlCommand cmdUpdate = new SqlCommand("spDeleteUser", con);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@Email", Email);
                    cmdUpdate.ExecuteNonQuery();
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
