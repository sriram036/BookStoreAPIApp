using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;

        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public UserModel AddUser(UserModel userModel)
        {
            return userRepo.AddUser(userModel);
        }

        public string LoginUser(LoginModel loginModel)
        {
            return userRepo.LoginUser(loginModel);
        }

        public ForgotPasswordModel ForgotPassword(string Email)
        {
            return userRepo.ForgotPassword(Email);
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            return userRepo.ResetPassword(Email, resetPasswordModel);
        }

        public UserModel UpdateUser(string Email, string Name, string Number)
        {
            return userRepo.UpdateUser(Email, Name, Number);
        }

        public bool DeleteUser(string Email)
        {
            return userRepo.DeleteUser(Email);
        }

        public UserModel getUser(int UserId)
        {
            return userRepo.getUser(UserId);
        }
    }
}
