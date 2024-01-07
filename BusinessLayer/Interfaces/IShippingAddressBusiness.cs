using ModelLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IShippingAddressBusiness
    {
        bool AddShipmentAddress(int UserId, ShipmentAddressModel shipmentAddressModel);
        List<ShipmentAddressModel> GetShippingAddress(int UserId);
        bool DeleteShippingAddress(int UserId, int ShippingId);
    }
}