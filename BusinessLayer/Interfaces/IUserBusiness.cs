using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        UserModel AddUser(UserModel userModel);
        UserModel LoginUser(LoginModel loginModel);
        ForgotPasswordModel ForgotPassword(string Email);
        bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
        UserModel UpdateUser(string Email, string Name, long Number);
        bool DeleteUser(string Email);
    }
}