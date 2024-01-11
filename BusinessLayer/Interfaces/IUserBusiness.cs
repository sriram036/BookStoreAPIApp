using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        UserModel AddUser(UserModel userModel);
        string LoginUser(LoginModel loginModel);
        ForgotPasswordModel ForgotPassword(string Email);
        bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
        UserModel UpdateUser(string Email, string Name, string Number);
        bool DeleteUser(string Email);
        UserModel getUser(int UserId);
    }
}