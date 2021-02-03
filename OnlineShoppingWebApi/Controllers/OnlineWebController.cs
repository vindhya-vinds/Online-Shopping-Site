using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShoppingWebApi.Models;

namespace OnlineShoppingWebApi.Controllers
{
    /// <summary>
    /// Online Shopping Site Web API Controller
    /// </summary>
    public class OnlineWebController : ApiController
    {
        /// <summary>
        /// Get the product details by the product name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>List of products</returns>
        [Route("api/OnlineWeb/GetProductByName/{name}")]
        public List<Product> GetProductByName(string name)
        {

            OnlineShoppingBll bll = new OnlineShoppingBll();
            var lstpd = bll.GetProductByName(name);
            return lstpd;
        }
        //[Route("api/OnlineWeb/GetAllProducts/")]
        /// <summary>
        /// Get all category details 
        /// <returns>List of categories</returns>
        public List<ProdCategory> Get()
        {
            OnlineShoppingBll bll = new OnlineShoppingBll();
            var lstpd = bll.GetAllDetails();
            return lstpd;
        }
        /// <summary>
        /// Getting the details of the product by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> returns product details</returns>
        [Route("api/OnlineWeb/GetDetailsOfProduct/{id}")]
        public Product GetDetailsOfProduct(int id)
        {
            OnlineShoppingBll bll = new OnlineShoppingBll();
            var lstpd = bll.GetDetailsOfProduct(id);
            return lstpd;
        }
        /// <summary>
        /// This method gives the all products in a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of products in the category</returns>
        [Route("api/OnlineWeb/GetProductByCategoryId/{id}")]
        public List<Products> GetProductByCategoryId(int id)
        {
            OnlineShoppingBll bll = new OnlineShoppingBll();
            var lstpd = bll.GetProductByCategoryId(id);
            return lstpd;
        }
        /// <summary>
        /// Adding the items to the cart
        /// </summary>
        /// <param name="cart"></param>
        [Route("api/OnlineWeb/AddCartItems")]
        public void Post([FromBody] List<Cart> c)
        {
            OnlineShoppingBll bll = new OnlineShoppingBll();
            bll.AddToCart(c);
        }
        /// <summary>
        /// Getting all the cart items
        /// </summary>
        /// <returns>List of cart items</returns>
        [Route("api/OnlineWeb/GetCartItems")]
        public List<Cart> GetCartItems()
        {
            OnlineShoppingBll bll = new OnlineShoppingBll();
            var lstitems = bll.GetCartDetails();
            return lstitems;
        }
        /// <summary>
        /// Delete the item from the cart by id
        /// </summary>
        /// <param name="id"></param>
        [Route("api/OnlineWeb/DeleteItem/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage errRes = Request.CreateErrorResponse(HttpStatusCode.OK, "Record deleted");
            try
            {
                OnlineShoppingBll bll = new OnlineShoppingBll();
                bll.DeleteProductById(id);
            }
            catch (Exception ex)
            {
                errRes = Request.CreateErrorResponse(HttpStatusCode.NotFound, "product id not found,could not delete");
            }
            return errRes;
        }
        /// <summary>
        /// Update the quantity of the product by id
        /// </summary>
        /// <param name="cart"></param>
        [Route("api/OnlineWeb/EditItem")]
        public HttpResponseMessage Put([FromBody] Cart cart)
        {
            HttpResponseMessage errRes = Request.CreateErrorResponse(HttpStatusCode.OK, "Record updated");
            try
            {
                OnlineShoppingBll bll = new OnlineShoppingBll();
                bll.UpdateQuantityById(cart);
            }
            catch (Exception ex)
            {
                errRes = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return errRes;
        }
    }
}
