using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public class OrderModel
    {
        public int CartId { get; set; }
        
        public int TotalPrice { get; set; }

        public int Quantity { get; set; }
    }
}
