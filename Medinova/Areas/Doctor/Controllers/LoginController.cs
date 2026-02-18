using Medinova.DTOs;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Medinova.Areas.Doctor.Controllers
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


            //bool passwordOk = Crypto.VerifyHashedPassword(
            //    user.Password,
            //    model.Password
            //geçici kapatıldı 

            bool passwordOk = user.Password == model.Password;

            if (!passwordOk)
            {
                ModelState.AddModelError("", "TC No veya Şifre Hatalı");
                return View(model);
            }

            bool isDoctor = user.Roles.Any(r => r.RoleName == "Doctor");

            if (!isDoctor)
            {
                ModelState.AddModelError("", "Bu giriş sadece doktorlar içindir.");
                return View(model);
            }

            var doctor = user.Doctors.FirstOrDefault();

            if (doctor == null)
            {
                ModelState.AddModelError("", "Doktor kaydı bulunamadı.");
                return View(model);
            }


            FormsAuthentication.SetAuthCookie(user.IdentityNumber, false);


            Session["DoctorFullName"] = $"{user.FirstName} {user.LastName}";
            Session["DoctorId"] = doctor.DoctorId;
            Session["DoctorTitle"] = doctor.Title;
            Session["DoctorImage"] = user.ImageUrl;

            TempData["SuccessLogin"] = $"{user.FirstName} {user.LastName} Hoşgeldiniz ";

            return RedirectToAction("Index", "Home", new { area = "Doctor" });
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Panel",new { area = ""});
        }

    }
}