using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWebApi.Models
{
    /// <summary>
    /// Cart class
    /// </summary>
    public class Cart
    {
        //ProductId
        public int ProductId { get; set; }
        //Product name
        public string ProductName { get; set; }
        //Product picture
        public string Picture { get; set; }
        //price of the product
        public decimal Price { get; set; }
        //quantity of the product
        public int Quantity { get; set; }
    }
}