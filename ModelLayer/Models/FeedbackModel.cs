using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public class FeedbackModel
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
