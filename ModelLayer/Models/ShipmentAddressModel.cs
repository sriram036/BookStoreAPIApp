using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public class ShipmentAddressModel
    {
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public int AddressType { get; set; }
    }
}
