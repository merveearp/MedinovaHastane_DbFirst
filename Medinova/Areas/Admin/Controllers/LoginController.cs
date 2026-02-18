using Medinova.DTOs;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Medinova.Areas.Admin
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        MedinovaContext context = new MedinovaContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = context.Users
                              .FirstOrDefault(x => x.IdentityNumber == model.IdentityNumber);

            if (user == null)
            {
                ModelState.AddModelError("", "TC No veya Şifre Hatalı");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError("", "TC No veya Şifre Hatalı");
                return View(model);
            }


            FormsAuthentication.SetAuthCookie(user.IdentityNumber, false);
            Session["FullName"] = user.FirstName + " " + user.LastName;
            Session["AdminId"] = user.UserId;
            Session["AdminProfile"] = user.ImageUrl;
    

           
            return RedirectToAction("Index", "Home",new {area="Admin"});

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Panel", new { area = "" });
        }
    }
}