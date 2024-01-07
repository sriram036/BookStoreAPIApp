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
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsAdded = orderBusiness.AddOrder(UserId, orderModel);

            if(IsAdded)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Order Placed", Data = "UserId and Quantity Constraint Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Order Not Placed", Data = "UserId or Quantity Constraint not Matched" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetOrders")]
        public List<OrderModel> GetOrderList()
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            List<OrderModel> orderModels = orderBusiness.GetOrderList(UserId);

            if(orderModels != null)
            {
                return orderModels;
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int OrderId)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsDeleted = orderBusiness.DeleteOrder(UserId, OrderId);

            if(IsDeleted)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Order Deleted", Data = "UserId Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Order Not Deleted", Data = "UserId Not Matched" });
            }
        }
    }
}
