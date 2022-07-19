using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginProject.Models;

namespace LoginProject.Controllers
{
    public class HomeController : Controller
    {
        gayathriEntities db = new gayathriEntities();
        // GET: Home

        public ActionResult Index()
        {
            return View(db.userinfoes.ToList());
        }
        public ActionResult Signup()
        {
            return View();
       }
        [HttpPost]
        public ActionResult Signup(userinfo userinfo)
        {
                if (db.userinfoes.Any(x => x.username == userinfo.username))
                {
                    ViewBag.Notification = "This account is already existed";
                return View();

            }
           
            else
            {
                db.userinfoes.Add(userinfo);
                db.SaveChanges();


                Session["idSS"] = userinfo.id.ToString();
                Session["usernameSS"] = userinfo.username.ToString();
                return RedirectToAction("Index", "Home");

            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userinfo userinfo)
        {
            var checkLogin = db.userinfoes.Where(x => x.username.Equals(userinfo.username) && x.password.Equals(userinfo.password)).FirstOrDefault();
            if(checkLogin !=null)
            {
                Session["idSS"] = userinfo.id.ToString();
                Session["usernameSS"] = userinfo.username.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "wrong username or password";
            }
            return View();
        }
        }
}