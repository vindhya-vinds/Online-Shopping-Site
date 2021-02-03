using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShoppingMVC.Models;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace OnlineShoppingMVC.Controllers
{
    public class OnlinemvcController : Controller
    {
        // GET: Onlinemvc
        [HttpGet]
        public ActionResult Index()
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:53719/api/");
            //call API for GET
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineWeb/Get").Result;
                //gets the products from search
                var lstc = JsonConvert.DeserializeObject<List<ProdCategory>>(result);
                //return the product details
                return View(lstc);
            }
            //return View();
        }
        [HttpPost]
        public ActionResult ProductList(string name)
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:53719/api/");
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineWeb/GetProductByName/" + name).Result;
                //get the product list
                var pd = JsonConvert.DeserializeObject<List<Product>>(result);
                //returns the details by product name
                return View("ProductList", pd);
            }
        }
        public ActionResult GetProductById(int id)
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:53719/api/");
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineWeb/GetDetailsOfProduct/" + id).Result;
                //get the product details by id clicking on the image
                var pd = JsonConvert.DeserializeObject<Product>(result);
                //returns the product details by id 
                return View(pd);
            }
        }
        //[HttpGet]
        public ActionResult Display(int id)
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:53719/api/");
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineWeb/GetProductByCategoryId/" + id).Result;
                //displays the products by clicking on category name
                var lstc = JsonConvert.DeserializeObject<List<Products>>(result);
                //displays the product by category name
                return View(lstc);
            }
        }
        [HttpGet]
        public ActionResult AddCart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCart(Cart c)
        {
            c.Quantity = 1;
            if (Session["cart"] == null)
            {
                List<Cart> lstc = new List<Cart>();
                lstc.Add(c);
                Session.Add("cart", lstc);
            }
            else
            {
                var lst = (List<Cart>)Session["cart"];
                lst.Add(c);
                Session.Add("cart", lst);
            }
            return RedirectToAction("DisplayCart");
        }
        public ActionResult DisplayCart()
        {
            var lstc = (List<Cart>)Session["cart"];
            Session.Add("cartList",lstc);
            return View(lstc);
        }
        /// <summary>
        /// Adding the items to the cart
        /// </summary>
        public ActionResult PostToCart()
        {
            Uri uri = new Uri("http://localhost:53719/api/");
            var lstc = (List<Cart>)Session["cartList"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                var result = client.PostAsJsonAsync<List<Cart>>("OnlineWeb/AddCartItems", lstc).Result;
                if (result.IsSuccessStatusCode == true)
                {
                    ViewData.Add("msg", "Posted");
                }
                else
                {
                    ViewData.Add("msg", "Error");
                }
            }
            return View(lstc);
        }
        /// <summary>
        /// Getting all the cart items
        /// </summary>
        /// <returns>List of cart items</returns>
        public ActionResult GetDetailsFromCart()
        {
            Uri uri = new Uri("http://localhost:53719/api/");
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineWeb/GetCartItems").Result;
                var lst = JsonConvert.DeserializeObject<List<Cart>>(result);
                return View(lst);
            }
        }
        public ActionResult DeleteFromCart(int id)
        {
            var cartLst = (List<Cart>)Session["cartList"];
            var prdt = cartLst.Where(o => o.ProductId == id).FirstOrDefault();
            cartLst.Remove(prdt);
            Session["cart"] = cartLst;
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Delete the item from the cart by id
        /// </summary>
        /// <param name="id"></param>
        public ActionResult DeleteFromDB(int id)
        {
            Uri uri = new Uri("http://localhost:53719/api/");
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                var result = client.DeleteAsync("OnlineWeb/DeleteItem/"+id).Result;
                //var lst = JsonConvert.DeserializeObject<List<Cart>>(result);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// Update the quantity of the product by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public ActionResult UpdateQuantity(int id)
        {
            var cartlst = (List<Cart>)Session["cart"];
            var prdt = cartlst.Where(o => o.ProductId == id).FirstOrDefault();
            return View(prdt);
        }
        /// <summary>
        /// Update the quantity of the product
        /// </summary>
        /// <param name="cart"></param>
        [HttpPost]
        public ActionResult UpdateQuantity(Cart cart)
        {
            var cartlst = (List<Cart>)Session["cart"];
            var prdt = cartlst.Where(o => o.ProductId==cart.ProductId).FirstOrDefault();
            prdt.Quantity = cart.Quantity;
            Session["cart"] = cartlst;            
            return RedirectToAction("DisplayCart");
        }
        //place the order of the product
        public ActionResult PlaceOrder()
        {
            return View();
        }
    }
}