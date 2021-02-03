using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWebApi.Models
{

    /// <summary>
    /// Product class
    /// </summary>
    public class Product
    {
        //Product Id
        public int ProductId { get; set; }
        //Category Id
        public int CategoryId { get; set; }
        //Product Name
        public string ProductName { get; set; }
        //Product Picture
        public string Picture { get; set; }
        //Product Price
        public decimal Price { get; set; }
        //Quantity of a Product
        public int Quantity { get; set; }
        //Product Content
        public string Content { get; set; }
    }
}