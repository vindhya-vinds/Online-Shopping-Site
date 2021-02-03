using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWebApi.Models
{
    /// <summary>
    /// Exception class
    /// </summary>
    public class OnlineException:Exception
    {
        public OnlineException(string errMsg):base(errMsg)
        {

        }
    }
}