using Medinova.DTOs.ProfileDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Medinova.Areas.Patient.Controllers
{
    public class ProfileController : BaseController
    {
        MedinovaContext context = new MedinovaContext();
        public ActionResult Index()
        {
            int userId = (int)Session["PatientUserId"];
            var today = DateTime.Today;

            var value = context.Users.Where(x => x.UserId == userId)
                .Select(x => new PatientProfileDto
                {
                    IdentityNumber = x.IdentityNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    ImageUrl = x.ImageUrl,

                    BirthDate = x.BirthDate,
                    Age = x.BirthDate != null
                                ? today.Year - x.BirthDate.Value.Year
                                : 0,

                    CreatedDate = x.CreatedDate,
                    GenderName = x.Gender.GenderName,
                    BloodType = x.BloodType,
                    IsActive = x.IsActive,
                    

                }).FirstOrDefault();

            return View(value);
        }


        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            int userId = (int)Session["PatientUserId"];

            var user = await context.Users.Where(x => x.UserId == userId)
                .Select(x => new PatientProfileEditDto
                {
                    UserId=userId,
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

            return RedirectToAction("Index", "Profile", new { area = "Patient" });
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {

            var userId  = (int)Session["PatientUserId"];
            return View(new PatientChangePasswordDto { UserId = userId });

        }

        [HttpPost]
        public ActionResult ChangePassword(PatientChangePasswordDto dto)
        {

            if(!ModelState.IsValid)
            {
                return View(dto);
            }

            int userId = (int)Session["PatientUserId"];
            var user = context.Users.Where(x => x.UserId == userId).FirstOrDefault();

            if(user.Password != dto.CurrentPassword)
            {
                ModelState.AddModelError("", "Mevcut şifre yanlış.");
                return View(dto);

            }

            user.Password = dto.NewPassword;
            context.SaveChanges();

            TempData["Success"] = "Şifreniz başarıyla güncellendi.";
            TempData["AlertType"] = "toast";


            return RedirectToAction("Index", "Profile", new { area = "Patient" });

        }

    }




}