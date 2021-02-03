using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWebApi.Models
{
    /// <summary>
    /// Product class
    /// </summary>
    public class Products
    {
        //Product Id
        public int ProductId { get; set; }
        //Product Name
        public string ProductName { get; set; }
        //Product Picture
        public string Picture { get; set; }
        //Product Price
       public decimal Price { get; set; }
    }
}