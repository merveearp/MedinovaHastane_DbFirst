
using Medinova.DTOs.AccountDtos;
using Medinova.DTOs.DoctorDtos;
using Medinova.Models;
using Medinova.Services.DoctorService;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;


namespace Medinova.Areas.Admin.Controllers
{
    public class DoctorController : Controller
    {
        MedinovaContext context = new MedinovaContext();
        private readonly IDoctorService _doctorService;
        public DoctorController()
        {
            _doctorService = new DoctorService();
        }

        public async Task<ActionResult> Index()
        {
            var values = await _doctorService.GetAllAsync();
            return View(values);
        }

        public async Task<ActionResult> ActiveDoctors()
        {
            var values = await _doctorService.GetAllAsync();
            return View("Index", values.Where(x => x.IsActive==true).ToList());
        }

        public async Task<ActionResult> PassiveDoctors()
        {
            var values = await _doctorService.GetAllAsync();
            return View("Index", values.Where(x => x.IsActive==false).ToList());
        }

        [HttpGet]
        public async Task<ActionResult> DoctorDetail(int id)
        {
            var value = await _doctorService.GetDetailByIdAsync(id);
            return View(value);
        }

        [HttpGet]
        public ActionResult DoctorEdit(int id)
        {
            var doctor = context.Doctors.FirstOrDefault(x => x.DoctorId == id);


            var today = DateTime.Today;

            var activeAppointmentCount = context.Appointments.Count(x =>
                x.DoctorId == id &&
                x.IsCompleted == false &&
                x.IsActive == true &&
                x.AppointmentDate >= today);

            var dto = new DoctorEditDto
            {
                DoctorId = doctor.DoctorId,
                UserId = doctor.UserId,
                DepartmentId = doctor.DepartmentId,
                Title = doctor.Title,
                IsActive = doctor.User.IsActive,
                ImageUrl = doctor.User.ImageUrl,
                FirstName = doctor.User.FirstName,
                LastName = doctor.User.LastName,
                ActiveAppointmentCount = activeAppointmentCount // 🔥
            };

            ViewBag.Departments = new SelectList(
                context.Departments.ToList(),
                "DepartmentId",
                "Name",
                dto.DepartmentId
            );

            return View(dto);
        }


        [HttpPost]
        public ActionResult DoctorEdit(DoctorEditDto doctorDto)
        {
            var doctor = context.Doctors.FirstOrDefault(x => x.DoctorId == doctorDto.DoctorId);
            
            if (doctor.User.IsActive && !doctorDto.IsActive)
            {
                var today = DateTime.Today;

                var appointments = context.Appointments
                    .Where(x =>
                        x.DoctorId == doctor.DoctorId &&
                        x.IsActive == true &&
                        x.IsCompleted == false &&
                        x.AppointmentDate >= today)
                    .ToList();

                foreach (var appointment in appointments)
                {
                    appointment.IsActive = false; 
                }

                TempData["CancelledCount"] = appointments.Count;
            }

            doctor.DepartmentId = doctorDto.DepartmentId;
            doctor.Title = doctorDto.Title;
            doctor.User.IsActive = doctorDto.IsActive;

            context.SaveChanges();

            return RedirectToAction("DoctorEdit", new { id = doctorDto.DoctorId });
        }


        [HttpGet]
        public ActionResult DoctorRegister()
        {
            ViewBag.Departments = new SelectList(context.Departments.ToList(),"DepartmentId","Name");
            return View();
        }

        [HttpPost]
        public ActionResult DoctorRegister(DoctorRegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var existingUser = context.Users.FirstOrDefault(x => x.IdentityNumber == register.IdentityNumber);
            if (existingUser != null)
            {
                ModelState.AddModelError("IdentityNumber", "Bu TC no ile daha önceden doktor kaydı bulunmaktadır.");
                return View(register);
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    string imageUrl = string.Empty; 

                    if (register.GenderId == 1)
                    {
                        imageUrl = "/Templates/medinova-1.0.0/img/doktor-avatarkadin.jpg";
                    }
                    else if (register.GenderId == 2)
                    {
                        imageUrl = "/Templates/medinova-1.0.0/img/doktor-avatar2.png";
                    }

                    var newUser = new User
                    {
                        IdentityNumber = register.IdentityNumber,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = $"{register.FirstName.ToLower()}@medinova.com",
                        PhoneNumber = register.PhoneNumber,
                        Password = Crypto.HashPassword($"{register.FirstName.ToLower()}medinova"),
                        BirthDate = register.BirthDate,
                        GenderId = register.GenderId,
                        ImageUrl = imageUrl,
                        IsActive = true,
                        BloodType = string.IsNullOrWhiteSpace(register.BloodType)
                                    ? "Ekleyiniz"
                                    : register.BloodType,
                        CreatedDate = DateTime.Now
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    var doctorRole = context.Roles.FirstOrDefault(x => x.RoleName == "Doctor");
                    if (doctorRole == null)
                        throw new Exception("Doctor rolü bulunamadı");

                    newUser.Roles.Add(doctorRole);

                    var doctor = new Models.Doctor
                    {
                        UserId = newUser.UserId,
                        DepartmentId = register.DepartmentId,
                        Title = register.Title,
                        Description = "Medinova Hastanesi bünyesinde görev yapan doktorumuz, hasta memnuniyetini ve etik değerleri ön planda tutarak modern tıp uygulamalarıyla hizmet vermektedir. Tanı ve tedavi süreçlerinde güncel bilimsel yaklaşımlar esas alınmaktadır."

                    };

                    context.Doctors.Add(doctor);

                    context.SaveChanges();
                    transaction.Commit();

                    return RedirectToAction("Index", "Doctor", new { area = "Admin" });
                }
                catch
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "Doktor kaydı sırasında hata oluştu.");
                    return View(register);
                }
            }
        }


        public async Task<ActionResult> GetListAppointment(int id)
        {
            var values = await _doctorService.GetListAppointmentByDoctorIdAsync(id);

            ViewBag.DoctorId = id;
     
            var doctorInfo = context.Doctors
                .Where(x => x.DoctorId == id)
                .Select(x => new
                {
                    x.Title,
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.ImageUrl
                })
                .FirstOrDefault();

            if (doctorInfo != null)
            {
                ViewBag.DoctorName =
                    $"{doctorInfo.Title} {doctorInfo.FirstName} {doctorInfo.LastName}";

                ViewBag.DoctorProfile = doctorInfo.ImageUrl;
            }
            else
            {
                ViewBag.DoctorName = "Doktor Bilgisi Bulunamadı";
                ViewBag.DoctorProfile = "/Templates/medinova-1.0.0/img/default-profile.png";
            }

            return View(values);
        }

        public async Task<ActionResult> GetAppointmentByDoctorIdAsync( int doctorId,int appointmentId)
        {        
            var value = await _doctorService.GetAppointmentByDoctorIdAsync(doctorId, appointmentId);
            ViewBag.appointmentId = appointmentId;
            return View(value);
        }

        public async Task<ActionResult> GetAppointmentDetailByDoctorAsync(int appointmentId,bool isCompleted)
        {
            ViewBag.AppointmentId = appointmentId;
            ViewBag.IsCompleted = isCompleted;
            var value = await _doctorService.GetAppointmentDetailByDoctorAsync(appointmentId, isCompleted);

            ViewBag.DoctorId = value.DoctorId;
            return View(value);
        }
    }
}