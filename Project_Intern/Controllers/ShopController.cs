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
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
    }
}