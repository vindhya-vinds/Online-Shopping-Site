using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWebApi.Models
{
    /// <summary>
    /// Product Category class
    /// </summary>
    public class ProdCategory
    {
        //Category Id
        public int CategoryId { get; set; }
        //Category Name
        public string CategoryName { get; set; }
    }
}