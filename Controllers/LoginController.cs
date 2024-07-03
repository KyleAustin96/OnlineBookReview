using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineBookReviews.Models;
using OnlineBookReviews.Utility;

namespace OnlineBookReviews.Controllers
{
    public class LoginController : Controller
    {
        DataStorageHelper data = new DataStorageHelper();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string emailAddress, string password)
        {
            using (BookEntities db = new BookEntities())
            {
                var user = db.UserDetail.FirstOrDefault(x => x.EmailAddress == emailAddress && x.Password == password);
                if(user != null)
                {
                    FormsAuthentication.SetAuthCookie((user.UserID.ToString()), true);
                    data.CreateData("UserID", user.UserID.ToString());
                    data.CreateData("FirstName", user.FirstName);
                    return RedirectToAction("Index", "BookDetails");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login");
                }
            }
                
            return View();
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}