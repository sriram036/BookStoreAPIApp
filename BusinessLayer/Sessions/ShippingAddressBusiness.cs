using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class ShippingAddressBusiness : IShippingAddressBusiness
    {
        private readonly IShippingAddressRepo shippingAddressRepo;

        public ShippingAddressBusiness(IShippingAddressRepo shippingAddressRepo)
        {
            this.shippingAddressRepo = shippingAddressRepo;
        }

        public bool AddShipmentAddress(int UserId, ShipmentAddressModel shipmentAddressModel)
        {
            return shippingAddressRepo.AddShipmentAddress(UserId, shipmentAddressModel);
        }

        public List<ShipmentAddressModel> GetShippingAddress(int UserId)
        {
            return shippingAddressRepo.GetShippingAddress(UserId);
        }

        public bool DeleteShippingAddress(int UserId, int ShippingId)
        {
            return shippingAddressRepo.DeleteShippingAddress(UserId, ShippingId);
        }
    }
}
