using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Intern.Models;

namespace Project_Intern.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public FruitShopEntities2 db = new FruitShopEntities2();
        public account account;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["account"] == null && filterContext.RouteData.Values["controller"].ToString() != "Member") 
            {
                filterContext.Result = new RedirectResult("Admin/Member/Login");
            }
            else
            {
                account = (account)Session["account"];
            }
            base.OnActionExecuting(filterContext);
        }


    }
}