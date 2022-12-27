using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project_Intern.Areas.Admin.Controllers
{
    public class MemberController : BaseController
    {
        // GET: Admin/Member
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckMember(FormCollection form)
        {
            string username = form["username"], password = CreateMD5Hash( form["password"]);
            var item = db.account.Where(m => m.username == username && m.password == password).FirstOrDefault();
            if (item == null)
            {
                return RedirectToAction("Login", "Member");
            }
            Session["account"] = item;
            return RedirectToAction("Index", "Home");
        }
        public string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public ActionResult Logout()
        {
            Session["account"] = null;
            return RedirectToAction("Login", "Member");
        }
    }
}