using Microsoft.AspNetCore.Mvc;
using OnlineShop_WebApp.Models;
using System.Collections.Generic;

using static OnlineShop_WebApp.Repos.DbContext;

namespace OnlineShop_WebApp.Controllers
{
    public class HomeController : Controller
    {
        IprContext repo;

        public HomeController(IprContext r)
        {
            repo = r;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var q = repo.GetProducts();
            return View(q);
        }

       

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
           // using (var memoryStream = new System.IO.MemoryStream())
           // {
               // await product.FileUpload.FormFile.CopyToAsync(memoryStream); // загрузка изображения на сервер. не работает!

             //   if (memoryStream.Length < 2097152)
               // {
                   // product.Image = memoryStream.ToArray();
                    repo.AddProduct(product);
                    return RedirectToAction("Index");
              //  }
               // else
               // {
                 //   ModelState.AddModelError("File", "The file is too large.");
                //    return RedirectToAction("Index");
            //    }
          //  }
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProductInfo(int id)
        {
            Product product = repo.GetProductById(id);
            ViewBag.Comments = repo.GetCommentsByProductID(id);
            return View(product);
        }

        public JsonResult SetComment(int id, string Comment)
        {
            
            repo.SetCommentToProduct(id, Comment);
            List<string> coments = repo.GetCommentsByProductID(id);
            return Json("");
        }

        
    }
}
