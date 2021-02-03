using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingMVC.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Content { get; set; }
    }
}