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
    public class WishListController : ControllerBase
    {
        private readonly IWishListBusiness wishListBusiness;

        public WishListController(IWishListBusiness wishListBusiness)
        {
            this.wishListBusiness = wishListBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("AddToWishList")]
        public IActionResult AddToWishList(int Id)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsAdded = wishListBusiness.AddToWishList(UserId, Id);

            if(IsAdded)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Added to WishList", Data = "UserId Matched and BookId Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Not Added to WishList", Data = "UserId or BookId Not Matched" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetWishList")]
        public List<int> GetWishList()
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            List<int> Books = wishListBusiness.GetWishList(UserId);

            if(Books != null)
            {
                return Books;
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteWishList")]
        public IActionResult DeleteWishList(int WishId)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsDeleted = wishListBusiness.DeleteWishList(WishId, UserId);

            if (IsDeleted)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "WishBook Deleted", Data = "UserId and BookId Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "WishBook Not Deleted", Data = "UserId or BookId Not Matched" });
            }
        }
    }
}
