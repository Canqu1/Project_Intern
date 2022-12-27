using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Intern.Models;
using PagedList;


namespace Project_Intern.Controllers
{
    public class ShopController : Controller
    {
        FruitShopEntities2 db = new FruitShopEntities2();
        // GET: Shop
        public ActionResult Index(int ? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.products
                         select l).OrderBy(x => x.product_id); 
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var product = db.products.ToList();
            ViewBag.Product = product;
           var category = db.categories.ToList();
            ViewBag.Category = category;
            return View(links.ToPagedList(pageNumber, pageSize));
        }   
        [HttpPost]
        public ActionResult Index(string cateid)
        {        

                ViewBag.selectedid = Int32.Parse(cateid);
                var category = db.categories.ToList();
                ViewBag.Categories = category;

                var products = db.products.ToList();
                if (cateid != "0")
                {
                    products = db.products
                        .Where(p => p.category_id == Int32.Parse(cateid))
                        .ToList();
                }
                return View(products);       
        }
        public ActionResult Product( int Page=1)
        {
            var product = db.products.ToList();
            ViewBag.Product = product;
            var category = db.categories.ToList();
            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public ActionResult Product(string cateid)
        {
            var products = db.products.ToList();
            if (cateid != "0")
            {
                products = db.products
                    .Where(p => p.category_id == Int32.Parse(cateid))
                    .ToList();
            }
            return View(products);
        }
    }
}