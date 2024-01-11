using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public class BookWithIdModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public double BookRating { get; set; }
        public int NoOfUsersRated { get; set; }
        public int BookOriginalPrice { get; set; }
        public int BookDiscountPrice { get; set; }
        public string BookDetail { get; set; }
        public string BookImage { get; set; }
        public int StockQuantity { get; set; }
        public int CartId { get; set; }
        public int CartQuantity { get; set; }
    }
}
