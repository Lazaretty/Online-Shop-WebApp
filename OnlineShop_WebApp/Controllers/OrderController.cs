using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OnlineShop_WebApp.Models;

using static OnlineShop_WebApp.Repos.DbContext;


namespace OnlineShop_WebApp.Controllers
{
    public class OrderController : Controller
    {
        IprContext repo;
 
        

        public OrderController(IprContext r)
        {
            repo = r;
        }

        [HttpGet]
        public IActionResult ShopList()
        {
            ViewBag.Amount =  repo.GetAmountOfProducts(Int32.Parse(HttpContext.Session.GetString("OrderId")));
            var Shoplist = repo.GetShopList(Int32.Parse(HttpContext.Session.GetString("OrderId")));
            return View(Shoplist);
        }

        private void NewOrder()
        {
            var OrderId = repo.NewOrder(Int32.Parse(HttpContext.Session.GetString("UserId"))).ToString();
            HttpContext.Session.SetString("OrderId", OrderId);
            HttpContext.Session.SetString("IsOrdering", "True"); ;

        }

        public IActionResult AddProductToList(int ProductId)
        {
            if (HttpContext.Session.GetString("IsOrdering") == null)
            {
                NewOrder();
            }

            repo.AddProductToOrder(Int32.Parse(HttpContext.Session.GetString("OrderId")), ProductId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ShopList(Order order)
        {
            order.UserId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            repo.Finalize(order);
            NewOrder();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetAllOrders()
        {

            return View();
        }
    }
}