using Medinova.DTOs.ProfileDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Medinova.Areas.Doctor.Controllers
{
    public class ProfileController : BaseController
    {
        MedinovaContext context = new MedinovaContext();
        public ActionResult Index()
        {
            var today = DateTime.Today;

            var value = context.Doctors
                .Where(x => x.DoctorId == DoctorId)
                .Select(x => new DoctorProfileDto
                {
                    DoctorId = x.DoctorId,
                    Title = x.Title,
                    Description = x.Description,
                    DepartmentName = x.Department.Name,

                    IdentityNumber = x.User.IdentityNumber,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    ImageUrl = x.User.ImageUrl,
                    BirthDate = x.User.BirthDate,
                    BloodType = x.User.BloodType,
                    Age = x.User.BirthDate != null
                                ? today.Year - x.User.BirthDate.Value.Year
                                : 0,
                    IsActive = x.User.IsActive,
                    CreatedDate = x.User.CreatedDate,
                    GenderName=x.User.Gender.GenderName

                }).FirstOrDefault();



            return View(value);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var value = context.Doctors
            .Where(x => x.DoctorId == DoctorId)
            .Select(x => new DoctorProfileEditDto
        {
            DoctorId = x.DoctorId,
            UserId = x.UserId,

            Title = x.Title,
            Description = x.Description,

            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Email = x.User.Email,
            PhoneNumber = x.User.PhoneNumber,
            BirthDate = x.User.BirthDate,
            BloodType = x.User.BloodType,
            ImageUrl = x.User.ImageUrl
        })
        .FirstOrDefault();

            return View(value);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorProfileEditDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var doctor = context.Doctors.FirstOrDefault(x => x.DoctorId == dto.DoctorId);

            var user = context.Users.FirstOrDefault(x => x.UserId == dto.UserId);
         
            doctor.Title = dto.Title;
            doctor.Description = dto.Description;

           
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.BirthDate = dto.BirthDate;
            user.BloodType = dto.BloodType;
            user.ImageUrl = dto.ImageUrl;

            context.SaveChanges();

            TempData["Success"] = "Profil bilgileri basariyla güncellendi.";

            return RedirectToAction("Index", "Profile", new { area = "Doctor" });

        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var userId = context.Doctors.Where(x => x.DoctorId == DoctorId).Select(x => x.UserId).FirstOrDefault();
            return View(new DoctorChangePasswordDto { UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(DoctorChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = context.Users.FirstOrDefault(x => x.UserId == dto.UserId);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(dto);
            }

            if (user.Password != dto.CurrentPassword)
            {
                ModelState.AddModelError("", "Mevcut şifre yanlış.");
                return View(dto);
            }

            user.Password = dto.NewPassword;
            context.SaveChanges();

            TempData["Success"] = "Şifreniz başarıyla güncellendi.";
            TempData["AlertType"] = "toast";


            return RedirectToAction("Index", "Profile", new { area = "Doctor" });

        }



    }
}