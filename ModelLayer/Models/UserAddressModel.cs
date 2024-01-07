using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public class UserAddressModel
    {
        public int UserAddressType { get; set; }

        public string UserAddress { get; set; }

        public string UserCity { get; set; }

        public string UserState { get; set; }
    }
}
