using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Intern.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project_Intern.Controllers
{
    public class LoginController : Controller
    {
       
        FruitShopEntities2 db = new FruitShopEntities2();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.account.Where(s => s.email.Equals(email) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().fullname;
                    Session["idUser"] = data.FirstOrDefault().a_id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Check(FormCollection form)
        {
            string email = form["email"], password = GetMD5(form["password"]);
            var item = db.account.Where(m => m.email == email && m.password == password).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Session["Login"] = item;

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Forgot()
        {
            return View();
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(account _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.account.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.account.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();


        }
         //Code mã hóa kí tự
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}