using Medinova.DTOs;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Medinova.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        MedinovaContext context = new MedinovaContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = context.Users
                .Include("Roles")
                .Include("Doctors")
                .Include("Patients")
                .FirstOrDefault(x => x.IdentityNumber == model.IdentityNumber);


            if (user == null || user.IdentityNumber != model.IdentityNumber)
            {
                TempData["LoginError"] = "Bu TC ye ait kayıt bulunamadı";
                return View(model);
            }

            if (user == null || user.Password != model.Password)
            {
                TempData["LoginError"] = "TC No veya Şifre Hatalı";
                return View(model);
            }

            if (!user.IsActive)
            {
                TempData["LoginError"] = "Hesabınız pasif.";
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.IdentityNumber, false);

           
            if (user.Roles.Any(r => r.RoleName == "Admin"))
            {
                Session["FullName"] = user.FirstName + " " + user.LastName;
                Session["AdminId"] = user.UserId;
                Session["AdminProfile"] = user.ImageUrl;

                Session["JustLoggedInAdmin"] = true;

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

        
            if (user.Roles.Any(r => r.RoleName == "Doctor"))
            {
                var doctor = user.Doctors.FirstOrDefault();

                Session["DoctorFullName"] = $"{user.FirstName} {user.LastName}";
                Session["DoctorId"] = doctor?.DoctorId;
                Session["DoctorTitle"] = doctor?.Title;
                Session["DoctorImage"] = user.ImageUrl;

                Session["JustLoggedInDoctor"] = true;

                return RedirectToAction("Index", "Home", new { area = "Doctor" });
            }

           
            if (user.Roles.Any(r => r.RoleName == "Patient"))
            {
                var patient = user.Patients.FirstOrDefault();

                Session["PatientName"] = user.FirstName + " " + user.LastName;
                Session["PatientImage"] = user.ImageUrl ?? "/Templates/medinova-1.0.0/img/default-profile.png";
                Session["PatientId"] = patient?.PatientId;
                Session["PatientUserId"] = patient?.UserId;
                Session["PatientTC"] = user.IdentityNumber;
               
                TempData["LoginSuccess"] = user.FirstName + " " + user.LastName + " " + "Giriş başarılı.";

                return RedirectToAction("Index", "Default", new { area = "" });
            }

            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var existingUser = context.Users.FirstOrDefault(x => x.IdentityNumber == register.IdentityNumber);
            if (existingUser != null)
            {

                ModelState.AddModelError("IdentityNumber", "Bu TC no ile daha önceden kayıt bulunmaktadır.");
                return View(register);
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    string imageUrl = string.Empty;

                    if (register.GenderId == 1)
                    {
                        imageUrl = "/Templates/medinova-1.0.0/img/hasta-avatar-kadın.jpg";
                    }
                    else if (register.GenderId == 2)
                    {
                        imageUrl = "/Templates/medinova-1.0.0/img/erkek-hastaavatar.png";
                    }

                    var newUser = new User
                    {
                        IdentityNumber = register.IdentityNumber,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = register.Email,
                        PhoneNumber = register.PhoneNumber,
                        Password = Crypto.HashPassword(register.Password),
                        GenderId = register.GenderId,
                        IsActive= true,
                        CreatedDate = DateTime.Now,
                        BirthDate =register.BirthDate,
                        ImageUrl = imageUrl
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges(); 


                    var patientRole = context.Roles.First(x => x.RoleName == "Patient");
                    newUser.Roles.Add(patientRole);

                    var patient = new Patient
                    {
                        UserId = newUser.UserId
                    };

                    context.Patients.Add(patient);

                    context.SaveChanges();
                    transaction.Commit();
                    

                    return RedirectToAction("Login", "Account",new {area =""});
                }
                catch
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu");
                    return View(register);

                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Default",new { area=""});
        }



    }
}