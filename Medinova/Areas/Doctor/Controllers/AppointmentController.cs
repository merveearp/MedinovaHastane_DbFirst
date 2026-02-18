using Medinova.DTOs;
using Medinova.DTOs.AppointmentDtos;
using Medinova.DTOs.DoctorDtos;
using Medinova.Enums;
using Medinova.Models;
using Medinova.Services.AppointmentService;
using Medinova.Services.DoctorService;
using Medinova.Services.MailService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Medinova.Areas.Doctor.Controllers
{
    public class AppointmentController : BaseController
    {
        MedinovaContext context = new MedinovaContext();

        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly IMailLogService _mailService;

        public AppointmentController()
        {
            _mailService = new MailLogService();
            _doctorService = new DoctorService();
            _appointmentService = new AppointmentService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _doctorService.GetListAppointmentByDoctorIdAsync(DoctorId);
            return View(values);
        }

        public async Task<ActionResult> ActiveAppointment()
        {
            var values = await _doctorService.GetListAppointmentByDoctorIdAsync(DoctorId);
            var filtered = values.Where(x => x.IsActive && !x.IsCompleted).ToList();
            return View("Index", filtered);
        }

        public async Task<ActionResult> PassiveAppointment()
        {
            var values = await _doctorService.GetListAppointmentByDoctorIdAsync(DoctorId);
            var filtered = values.Where(x => !x.IsActive && !x.IsCompleted).ToList();
            return View("Index", filtered);
        }

        public async Task<ActionResult> IsCompletedAppointment()
        {
            var values = await _doctorService.GetListAppointmentByDoctorIdAsync(DoctorId);
            var filtered = values.Where(x => !x.IsActive && x.IsCompleted).ToList();
            return View("Index", filtered);
        }

        public async Task<ActionResult> TodayAppointment()
        {
            var values = await _doctorService.GetListAppointmentByDoctorIdAsync(DoctorId);
            ViewBag.AppointmentDate = DateTime.Today;

            var filtered = values
                .Where(x => x.IsActive && x.AppointmentDate.Date == DateTime.Today)
                .ToList();

            return View("Index", filtered);
        }



        public async Task<ActionResult> AppointmentDetail(int appointmentId)
        {
            int doctorId = (int)Session["DoctorId"];
            var value = await _doctorService.GetAppointmentByDoctorIdAsync(doctorId, appointmentId);          
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> CompleteAppointment(GetAppointmentInfoDto dto)
        {
            dto.IsActive = false;
            dto.IsCompleted = true;

            await _doctorService.UpdateAppointmentByDoctorIdAsync(dto);

            return RedirectToAction("Create","AppointmentDetail",
                new { appointmentId = dto.AppointmentId });
        }

        [HttpPost]
        public async Task<ActionResult> CanceledAppointment(GetAppointmentInfoDto dto)
        {
            await _doctorService.CanceledAppointmentByDoctorIdAsync(dto);

            var appointment = context.Appointments.Where(x => x.AppointmentId == dto.AppointmentId).FirstOrDefault();

            TempData["CancelSuccess"] = $"{appointment.AppointmentDate.ToShortDateString()} tarihli {appointment.AppointmentTime} saatindeki randevu kaydı iptal edilmiştir.";

            return RedirectToAction("AppointmentDetail",
                new { appointmentId = dto.AppointmentId });          
        }

        public void AppointmentListItem()
        {
            var departmentName= context.Doctors.Where(x => x.DoctorId == DoctorId).Select(x => x.Department.Name).FirstOrDefault();
            ViewBag.DepartmentName = departmentName ?? "Bölüm Bilgisi Bulunamadı";


            var dateList = new List<SelectListItem>();

            for (int i = 0; i < 10; i++)
            {
                var date = DateTime.Now.AddDays(i);

                dateList.Add(new SelectListItem
                {
                    Text = date.ToString("dd.MMMM.dddd"),
                    Value = date.ToString("yyyy-MM-dd")
                });
            }
            ViewBag.dateList = dateList;             

        }
        public void PatientList()
        {
            var patientList = context.Appointments
               .Where(x => x.DoctorId == DoctorId && x.Patient.User.IsActive)
               .Select(x => new
               {
                   x.PatientId,
                   FullName = x.Patient.User.FirstName + " " + x.Patient.User.LastName
               })
               .Distinct()
               .ToList();



            if (patientList == null || !patientList.Any())
            {
                ViewBag.PatientList = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                    Text = "Kayıtlı hasta bulunmamaktadır",
                    Value = ""
                    }
                };

            }
            else
            {
                ViewBag.PatientList = patientList.Select(x => new SelectListItem
                {
                    Value = x.PatientId.ToString(),
                    Text = x.FullName,
                }).ToList();


            }


        }

        [HttpGet]
        public JsonResult GetAvailableHours(DateTime selectedDate)
        {
            int doctorId = DoctorId;

            var bookedTimes = context.Appointments
                .Where(x =>
                    x.DoctorId == doctorId &&
                    DbFunctions.TruncateTime(x.AppointmentDate) == selectedDate.Date &&
                    x.IsActive == true)
                .Select(x => x.AppointmentTime)
                .ToList();

            var result = Times.AppointmentHour
                .Select(hour => new
                {
                    Time = hour,
                    IsBooked = bookedTimes.Contains(hour)
                }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateAppointment()
        {
            PatientList();
            AppointmentListItem();              
            return View();
            
        }

        [HttpPost]
        public async Task<ActionResult> CreateAppointment(CreateAppointmentDto dto)
        {

            if(!ModelState.IsValid)
            {
                PatientList();   
                AppointmentListItem();
                return View(dto);
            }

            try
            {
                await _appointmentService.CreateAppointmentByDoctorAsync(dto, DoctorId);

                TempData["Success"] = "Randevu başarıyla oluşturuldu.";
                return RedirectToAction("Index","Home",new {area = "Doctor"});


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                PatientList();
                AppointmentListItem();
                return View(dto);
            }          

        }

        [HttpGet]
        public ActionResult CreateAppointmentByPatient(int patientId)
        {
            AppointmentListItem();
            var dto = new CreateAppointmentDto
            {
                PatientId = patientId
                
            };


            ViewBag.PatientId = patientId;
            var patient = context.Patients.Where(x => x.PatientId == patientId).FirstOrDefault();

            ViewBag.PatientImage = patient.User.ImageUrl;
            ViewBag.PatientName= patient.User.FirstName + " " + patient.User.LastName;
            ViewBag.PatientTC= patient.User.IdentityNumber;

            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAppointmentByPatient(CreateAppointmentDto dto)
        {


            ViewBag.PatientId = dto.PatientId;
            var patient = context.Patients.Where(x => x.PatientId == dto.PatientId).FirstOrDefault();

            ViewBag.PatientImage = patient.User.ImageUrl;
            ViewBag.PatientName = patient.User.FirstName + " " + patient.User.LastName;
            ViewBag.PatientTC = patient.User.IdentityNumber;
            if (!ModelState.IsValid)
            {
                AppointmentListItem();
                return View(dto);
            }

            try
            {
                await _appointmentService
                    .CreateAppointmentByDoctorByPatientAsync(dto, DoctorId, dto.PatientId);

                TempData["Success"] = "Randevu başarıyla oluşturuldu.";

                return RedirectToAction("Index", "Appointment", new { area = "Doctor" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                AppointmentListItem();
                return View(dto);
            }
        }





    }
}
