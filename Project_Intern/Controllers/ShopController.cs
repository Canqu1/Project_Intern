using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Intern.Models;


namespace Project_Intern.Controllers
{
    public class ShopController : Controller
    {
        FruitShopEntities db = new FruitShopEntities();
        // GET: Shop
        public ActionResult Index()
        {
            List<products> product = db.products.ToList();
            ViewBag.Product = product;
            return View();
        }   
        public ActionResult Product()
        {
            List<products> product = db.products.ToList();
            ViewBag.Product = product;
            return View();
        }
    }
}