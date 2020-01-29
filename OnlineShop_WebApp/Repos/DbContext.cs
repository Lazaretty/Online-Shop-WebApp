using OnlineShop_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace OnlineShop_WebApp.Repos
{
    public class DbContext
    {
        public interface IprContext
        {
            List<Product> GetProducts();
            void AddProduct(Product product);
            Product GetProductById(int id);
            List<string> GetCommentsByProductID(int id);
            void SetCommentToProduct(int id, string Comment);
            bool IsRegisted(string Email);
            void AddUser(UserM User);
            bool Login(Login login);
            UserM GetUser(Login login);
            void AddProductToOrder(int UserId, int ProducrId);
            int NewOrder(int UserId);
            List<Product> GetShopList(int OrderId);
            List<ShopList> GetAmountOfProducts(int OrderId);
            void Finalize(Order order);
        }

        public class PrContext : IprContext
        {
            string connectionString = null;

            public PrContext(string conn)
            {
                connectionString = conn;
            }

            public List<Product> GetProducts()
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<Product>("ShowAllProducts", commandType: CommandType.StoredProcedure).ToList();
                }
            }

            public Product GetProductById(int id)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("ProdID", id);
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<Product>("GetProductById", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }


            public List<string> GetCommentsByProductID(int id)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("ProdID", id);
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<string>("GetCommentsByProductID", parameter, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            public void SetCommentToProduct(int id, string Comment)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("ProdID", id);
                parameter.Add("Comment", Comment);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("SetCommentToProduct", parameter, commandType: CommandType.StoredProcedure);
                }

            }

            public void AddProduct(Product product)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("about", product.About);
                parameter.Add("IdSubCategory", product.IdSubCategory);
                parameter.Add("NameOfProduct", product.NameOfProduct);
                parameter.Add("Price", product.Price);
                parameter.Add("Producer", product.Producer);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("Addproduct", parameter, commandType: CommandType.StoredProcedure);
                }
            }

            public bool IsRegisted(string Email)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("EM", Email);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<UserM>("IsRegisted", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault()!=null;
                }
            }

            public void AddUser(UserM User)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("Email", User.Email);
                User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);
                parameter.Add("Password", User.Password);
                parameter.Add("FIO", User.FIO);
                parameter.Add("DateOfBirthDay", User.DateOfBirthDay);
                parameter.Add("AboutMe", User.AboutMe);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("AddUser", parameter, commandType: CommandType.StoredProcedure);
                }

            }

            public bool Login(Login login) // Возвращает TRUE, если пользователь с такой комбинауий e-mail и пароль сущетсвует в БАЗЕ
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("EM",
                    login.Email);
                
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var User =  db.Query<UserM>("IsRegisted", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    if (User == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (BCrypt.Net.BCrypt.Verify(login.Password, User.Password))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            public UserM GetUser(Login login)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("EM", login.Email);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<UserM>("IsRegisted", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }

            }

            public int NewOrder(int UserId)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("UserId", UserId);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<int>("NewOrder", parameter, commandType: CommandType.StoredProcedure).Max();
                }
            }

            public void AddProductToOrder(int OrderId, int ProducrId)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("OrderId", OrderId);
                parameter.Add("ProductId", ProducrId);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("AddProductToOrder", parameter, commandType: CommandType.StoredProcedure);
                }
            }

            public List<Product> GetShopList(int OrderId)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("OrderId", OrderId);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<Product>("GetShopList", parameter, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            public List<ShopList> GetAmountOfProducts(int OrderId)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("OrderId", OrderId);
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<ShopList>("GetAmountOfProducts", parameter, commandType: CommandType.StoredProcedure).ToList();
                }

            }

            public void Finalize(Order order)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("UserId", order.UserId);
                parameter.Add("PaymentMethod", order.PaymentMethod);
                parameter.Add("DeliveryAddress", order.DeliveryAddress);
                
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("FinalOrder", parameter, commandType: CommandType.StoredProcedure);
                }
            }

        }


    }
}
