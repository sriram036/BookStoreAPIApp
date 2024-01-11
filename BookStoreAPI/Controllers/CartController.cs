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
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness cartBusiness;

        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart(CartModel cartModel)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsAdded = cartBusiness.AddToCart(UserId, cartModel);

            if(IsAdded)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Book Added to the Cart", Data = "UserId Matched and Book Id Matched"});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Book Not Added to the Cart", Data = "UserId Not Matched or BookId Not in the List" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetCartsByUser")]
        public List<BookWithIdModel> GetCartBooksByUserId()
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);

            List<BookWithIdModel> carts = cartBusiness.GetCartBooksByUserId(UserId);

            if (carts != null)
            {
                return carts;
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetCarts")]
        public List<CartModel> GetCarts()
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);

            List<CartModel> carts = cartBusiness.GetCarts(UserId);

            if(carts != null)
            {
                return carts;
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateCart")]
        public IActionResult UpdateCart(int count, int cartId)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsUpdated = cartBusiness.UpdateCart(count, cartId, UserId);

            if (IsUpdated)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Count Updated", Data = "User Id Matched"});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Data Not Updated", Data = "User Id Not Matched" });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart(int cartId)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsDeleted = cartBusiness.DeleteCart(cartId, UserId);
            if(IsDeleted)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Cart Deleted Successfully", Data = "UserId Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Cart Not Deleted", Data = "UserId Not Matched or Cart Id Not in the List" });
            }
        }
    }
}
