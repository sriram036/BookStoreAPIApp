using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressBusiness userAddressBusiness;

        public UserAddressController(IUserAddressBusiness userAddressBusiness)
        {
            this.userAddressBusiness = userAddressBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("AddUserAddress")]
        public IActionResult AddUserAddress(UserAddressModel userAddressModel)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            UserAddressModel userAddress = userAddressBusiness.AddUserAddress(UserId, userAddressModel);

            if(userAddress == null)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Address Not Added", Data = "Type Already Exist or Not Matched with Constraint" });
            }
            else
            {
                return Ok(new ResponseModel<UserAddressModel> { IsSuccess = true, Message = "Address Added", Data = userAddress });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserAddresses")]
        public List<UserAddressModel> GetUserAddresses()
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            List<UserAddressModel> userAddresses = userAddressBusiness.GetUserAddresses(UserId);
            if(userAddresses != null)
            {
                return userAddresses;
            }

            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateUserAddress")]
        public IActionResult UpdateUserAddress(UserAddressModel userAddressModel)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsUpdated = userAddressBusiness.UpdateUserAddress(UserId, userAddressModel);

            if(IsUpdated)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Address Updated", Data = "Type constraint Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Address Not Updated", Data = "Type constraint not Matched" });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteUserAddress")]
        public IActionResult DeleteUser(int Type)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsDeleted = userAddressBusiness.DeleteUser(UserId, Type);

            if (IsDeleted)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "User Address Deleted", Data = "Type Constraint Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "User Address Not Deleted", Data = "Type constraint not Matched" });
            }
        }
    }
}
