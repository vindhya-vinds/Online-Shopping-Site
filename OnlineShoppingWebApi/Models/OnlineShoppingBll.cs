using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWebApi.Models
{
    /// <summary>
    /// Online Shopping Site Business Layer
    /// </summary>
    public class OnlineShoppingBll
    {
        /// <summary>
        /// Get the product details by the product name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>List of products</returns>
        public List<Product> GetProductByName(string pname)
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                var lstpd = dal.GetProductByName(pname);
                return lstpd;
            }
            catch(Exception ex)
            {
                throw ex;
            }          
        }
        /// <summary>
        /// Get all category details 
        /// <returns>List of categories</returns>
        public List<ProdCategory> GetAllDetails()
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                var lstpd = dal.GetAllDetails();
                return lstpd;
            }
             catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Getting the details of the product by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> returns product details</returns>
        public Product GetDetailsOfProduct(int id)
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                var lstpd = dal.GetDetailsOfProduct(id);
                return lstpd;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// This method gives the all products in a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of products in the category</returns>
        public List<Products> GetProductByCategoryId(int id)
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                var lstpd = dal.GetProductByCategoryId(id);
                return lstpd;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Adding the items to the cart
        /// </summary>
        /// <param name="cart"></param>
        public void AddToCart(List<Cart> cart)
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                dal.AddToCart(cart);
            }
            catch(OnlineException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Getting all the cart items
        /// </summary>
        /// <returns>List of cart items</returns>
        public List<Cart> GetCartDetails()
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                var lst=dal.GetCartItems();
                return lst;
            }
            catch (OnlineException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete the item from the cart by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProductById(int id)
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                 dal.DeleteProductById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Update the quantity of the product by id
        /// </summary>
        /// <param name="cart"></param>
        public void UpdateQuantityById(Cart cart)
        {
            try
            {
                OnlineShoppingDAL dal = new OnlineShoppingDAL();
                dal.UpdateQuantityById(cart);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}