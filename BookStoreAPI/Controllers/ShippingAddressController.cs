using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressBusiness shippingAddressBusiness;

        public ShippingAddressController(IShippingAddressBusiness shippingAddressBusiness)
        {
            this.shippingAddressBusiness = shippingAddressBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("AddShippingAddress")]
        public IActionResult AddShipmentAddress(ShipmentAddressModel shipmentAddressModel)
        {
            int UserId = int.Parse(User.FindFirst("UserId").Value);
            bool IsAdded = shippingAddressBusiness.AddShipmentAddress(UserId, shipmentAddressModel);

            if(IsAdded)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Address Added", Data = "AddressType constraint Matched" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Address Not Added", Data = "AddressType constraint Wrong" });
            }
        }
    }
}
