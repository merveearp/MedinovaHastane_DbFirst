using Medinova.DTOs.ProfileDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        MedinovaContext context = new MedinovaContext();

        public ActionResult Index()
        {

            int adminId = (int)Session["AdminId"];
            var today = DateTime.Today;

            var value = context.Users
                .Where(x => x.UserId == adminId)
                .Select(x => new DoctorProfileDto
                {
                    IdentityNumber = x.IdentityNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    ImageUrl = x.ImageUrl,
                    BirthDate = x.BirthDate,
                    BloodType = x.BloodType,

                    Age = x.BirthDate != null
                        ? today.Year - x.BirthDate.Value.Year
                        : 0,

                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    GenderName = x.Gender.GenderName
                })
                .FirstOrDefault();

            return View(value);
        }


        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            int adminId = (int)Session["AdminId"];

            var user = await context.Users.Where(x => x.UserId == adminId)
                .Select(x => new PatientProfileEditDto
                {
                    UserId = adminId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    BirthDate = x.BirthDate,
                    BloodType = x.BloodType,
                    ImageUrl = x.ImageUrl,



                }).FirstOrDefaultAsync();

            return View(user);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(PatientProfileEditDto dto)
        {

           
            if (!ModelState.IsValid)
                return View(dto);

            var user = context.Users.FirstOrDefault(x => x.UserId == dto.UserId);

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.BirthDate = dto.BirthDate;
            user.BloodType = dto.BloodType;
            user.ImageUrl = dto.ImageUrl;

            context.SaveChanges();

            TempData["Success"] = "Profil bilgileri başariyla güncellendi.";

            return RedirectToAction("Index", "Profile", new { area = "Admin" });
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {

            int adminId = (int)Session["AdminId"];
            return View(new PatientChangePasswordDto { UserId = adminId });

        }

        [HttpPost]
        public ActionResult ChangePassword(PatientChangePasswordDto dto)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            int adminId = (int)Session["AdminId"];
            var user = context.Users.Where(x => x.UserId == adminId).FirstOrDefault();

            if (user.Password != dto.CurrentPassword)
            {
                ModelState.AddModelError("", "Mevcut şifre yanlış.");
                return View(dto);

            }

            user.Password = dto.NewPassword;
            context.SaveChanges();

            TempData["Success"] = "Şifreniz başarıyla güncellendi.";
            TempData["AlertType"] = "toast";


            return RedirectToAction("Index", "Profile", new { area = "Admin" });

        }


    }
}


