using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IShippingAddressBusiness
    {
        bool AddShipmentAddress(int UserId, ShipmentAddressModel shipmentAddressModel);
    }
}