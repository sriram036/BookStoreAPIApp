using ModelLayer.Models;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IShippingAddressRepo
    {
        bool AddShipmentAddress(int UserId, ShipmentAddressModel shipmentAddressModel);
        List<ShipmentAddressModel> GetShippingAddress(int UserId);
        bool DeleteShippingAddress(int UserId, int ShippingId);
    }
}