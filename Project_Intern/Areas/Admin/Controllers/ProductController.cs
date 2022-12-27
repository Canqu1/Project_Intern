using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Intern.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getProduct()
        {
            var items = db.products.ToList().Select(n => new List<string>
            {
                n.product_id.ToString(),
                n.product_name,
                n.product_quantity.ToString(),
                db.categories.Find(n.category_id).category_name,
                n.date_manufacture.ToString("dd-MM-yyyy"),
                n.date_expriration.ToString("dd-MM-yyyy"),
                n.list_price.ToString(),     
                n.product_description
            }).ToList();
            return new JsonResult() { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Edit(Guid? id)
        {
            Project_Intern.Models.products item = new Models.products();
            if (id != null) {
                item = db.products.Find(id);
            }
            return View(item);
        }
    }
}