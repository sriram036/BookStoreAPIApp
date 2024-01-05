using ModelLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IShippingAddressRepo
    {
        bool AddShipmentAddress(int UserId, ShipmentAddressModel shipmentAddressModel);
    }
}