using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(UserModel userModel)
        {
            UserModel User = userBusiness.AddUser(userModel);
            if(User != null)
            {
                return Ok(new ResponseModel<UserModel> { IsSuccess = true, Message = "Data Added Successfully", Data = User});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Data Not Added", Data = "Email Already Exist" });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            UserModel User = userBusiness.LoginUser(loginModel);
            if(User.Email == loginModel.Email)
            {
                return Ok(new ResponseModel<UserModel> { IsSuccess = true, Message = "Login Successfull", Data = User });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Login Failed", Data = "Email Not Exist or Password Not Matched" });
            }
        }

        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string Email) 
        {
            ForgotPasswordModel token = userBusiness.ForgotPassword(Email);
            if(token != null)
            {
                return Ok(new ResponseModel<ForgotPasswordModel> { IsSuccess = true, Message = "Mail Sent Successfully", Data = token });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Mail Not Sent", Data = "Email Not Found" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string Email = User.FindFirst("Email").Value;
            bool IsSuccess = userBusiness.ResetPassword(Email, resetPasswordModel);
            if(IsSuccess)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Password Updated", Data = Email });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Password Not Updated", Data = "Email Not Matched" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(UserModel user)
        {
            string Email = User.FindFirst("Email").Value;
            UserModel userModel = userBusiness.UpdateUser(Email, user.FullName, user.MobileNumber);

            if(userModel != null)
            {
                return Ok(new ResponseModel<UserModel> { IsSuccess = true, Message = "Data Updated", Data = userModel });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Data Not Updated", Data = "User Not Found" });
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            string Email = User.FindFirst("Email").Value;
            bool IsDeleted = userBusiness.DeleteUser(Email);

            if (IsDeleted)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "User Deleted Successfully", Data = "Email Found and Matched"});
            }

            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "User Not Deleted", Data = "Email Not Found or Matched" });
            }
        }
    }
}
