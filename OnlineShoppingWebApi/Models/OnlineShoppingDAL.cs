using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace OnlineShoppingWebApi.Models
{
    public class OnlineShoppingDAL
    {

        SqlConnection con;
        SqlCommand cmd;
        public OnlineShoppingDAL()
        {
            //1.configure connection object
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HCLDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        /// <summary>
        /// Get the product details by the product name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>List of products</returns>
        public List<Product> GetProductByName(string productName)
        {
            List<Product> lstPd = new List<Product>();
            try
            {
                //configure command for SELECT ALL
                cmd = new SqlCommand();
                cmd.CommandText = "select * from product where pname like @pn";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@pn", productName);
                cmd.CommandType = CommandType.Text;
                //Attach the connection with the command
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                Product pd;
                //read the records from data records and add them to the list
                while (sdr.Read())
                {
                    pd = new Product
                    {
                        ProductId = (int)sdr[0],
                        CategoryId = (int)sdr[1],
                        ProductName = sdr[2].ToString(),
                        Picture = sdr[3].ToString(),
                        Price = (decimal)sdr[4],
                        Quantity = (int)sdr[5],
                        Content = sdr[6].ToString(),
                    };
                    lstPd.Add(pd);
                }
                //close the data reader
                sdr.Close();
            }
            catch(SqlException ex)
            {
                 throw new OnlineException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            return lstPd;
        }
        /// <summary>
        /// Get all category details 
        /// <returns>List of categories</returns>
        public List<ProdCategory> GetAllDetails()
        {
            List<ProdCategory> lstcate = new List<ProdCategory>();
            try
            {
                //configure command for SELECT ALL
                cmd = new SqlCommand();
                cmd.CommandText = "select * from prodcategory";
                cmd.CommandType = CommandType.Text;
                //Attach the connection with the command
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                //read the records from data records and add them to the list
                while (sdr.Read())
                {
                    ProdCategory cobj = new ProdCategory
                    {
                        CategoryId = (int)sdr[0],
                        CategoryName = sdr[1].ToString()
                    };
                    lstcate.Add(cobj);
                }
                //close the data reader
                sdr.Close();
            }
            catch(SqlException ex)
            {
                throw new OnlineException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            return lstcate;
        }
        /// <summary>
        /// Getting the details of the product by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> returns product details</returns>
        public Product GetDetailsOfProduct(int id)
        {
            Product pd = new Product();
            try
            {
                //configure command for SELECT ALL
                cmd = new SqlCommand();
                cmd.CommandText = "select *from product where pid=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;
                //Attach the connection with the command
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                //read the records from data records and add them to the list
                while (sdr.Read())
                {
                    pd.ProductId = (int)sdr[0];
                    pd.CategoryId = (int)sdr[1];
                    pd.ProductName = sdr[2].ToString();
                    pd.Picture = sdr[3].ToString();
                    pd.Price = (decimal)sdr[4];
                    pd.Quantity = (int)sdr[5];
                    pd.Content = sdr[6].ToString();
                }
                //close the data reader
                sdr.Close();
            }
            catch(SqlException ex)
            {
                throw new OnlineException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            return pd;
        }
        /// <summary>
        /// This method gives the all products in a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of products in the category</returns>
        public List<Products> GetProductByCategoryId(int id)
        {
            List<Products> lstpd = new List<Products>();
            try
            {
                //configure command for SELECT ALL
                cmd = new SqlCommand();
                cmd.CommandText = "select p.pid,p.pname,p.picture,p.price from product as p join" +
                                   " prodcategory as c on p.cid=c.cid and p.cid like @ci";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ci", id);
                cmd.CommandType = CommandType.Text;
                //Attach the connection with the command
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                //read the records from data records and add them to the list
                while (sdr.Read())
                {
                    Products pobj = new Products
                    {
                        ProductId = (int)sdr[0],
                        ProductName = sdr[1].ToString(),
                        Picture = sdr[2].ToString(),
                        Price = (decimal)sdr[3]
                    };
                    lstpd.Add(pobj);
                }
                //close the data reader
                sdr.Close();
            }
            catch(SqlException ex)
            {
                throw new OnlineException(ex.Message);
            }           
            catch(Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }           
            return lstpd;
        }
        /// <summary>
        /// Adding the items to the cart
        /// </summary>
        /// <param name="cart"></param>
        public void AddToCart(List<Cart> cart)
        {
            int items;
            try
            {
                //configure command for INSERT statement
                cmd = new SqlCommand();
                //attach connection with the command
                cmd.Connection = con;
                //open the connection
                con.Open();
                foreach (var item in cart)
                {
                    
                    cmd.CommandText = "insert into Cart(pid,pname,picture,price) values(@pid,@pname,@picture,@price)";
                    //supply values to the parameters of the command
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@pid", item.ProductId);
                    cmd.Parameters.AddWithValue("@pname", item.ProductName);
                    cmd.Parameters.AddWithValue("@picture", item.Picture);
                    cmd.Parameters.AddWithValue("@price", item.Price);
                    // cmd.Parameters.AddWithValue("@content", item.Content);

                    //specify the type of command
                    cmd.CommandType = CommandType.Text;
                     //execute the command
                    items = cmd.ExecuteNonQuery();
                    if (items == 0)
                    {
                        throw new Exception("Item can't be added to cart");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new OnlineException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }   
        }
        /// <summary>
        /// Getting all the cart items
        /// </summary>
        /// <returns>List of cart items</returns>
        public List<Cart> GetCartItems()
        {
            List<Cart> lst = new List<Cart>();
            try
            {
                // configure command for SELECT ALL
                cmd = new SqlCommand();
                cmd.CommandText = "select * from Cart";
                cmd.CommandType = CommandType.Text;
                //attach connection with the command
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                //read the records from data records and add them to the list
                while (sdr.Read())
                {
                    Cart cobj = new Cart
                    {
                        ProductId=(int)sdr[0],
                        ProductName=sdr[1].ToString(),
                        Picture=sdr[2].ToString(),
                        Price=(decimal)sdr[3]
                    };
                    lst.Add(cobj);
                }
                //close the data reader
                sdr.Close();
            }
            catch (SqlException ex)
            {
                throw new OnlineException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            return lst;
        }
        /// <summary>
        /// Delete the item from the cart by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProductById(int id)
        {
            try
            {
                //configure command for DELETE
                cmd = new SqlCommand();
                cmd.CommandText = "delete from Cart where pid=@pid";
                //pass the value to the parameter
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@pid", id);
                cmd.CommandType = CommandType.Text;
                //attach connection to the command
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                int recordsAffected = cmd.ExecuteNonQuery();
                //con.Close();

                if (recordsAffected == 0)
                {
                    throw new Exception("Product Id does not exist");
                }
            }
            catch(SqlException ex)
            {
                throw new OnlineException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
        }
        /// <summary>
        /// Update the details of the product by id
        /// </summary>
        /// <param name="cart"></param>
        public void UpdateQuantityById(Cart cart)
        {
            try
            {
                //configure command for UPDATE
                cmd = new SqlCommand();
                cmd.CommandText = "update Cart set quantity=@qty where pid=@pid";
                //specify the command type                
                cmd.CommandType = CommandType.Text;
                //supply values to the parameters
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@qty", cart.Quantity);
                //attach the connection with command
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                int recordsAffected = cmd.ExecuteNonQuery();
                if (recordsAffected == 0)
                {
                    throw new Exception("product id does not exist");
                }

            }
            catch (SqlException ex)
            {
                throw new OnlineException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new OnlineException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }

        }
    }
}