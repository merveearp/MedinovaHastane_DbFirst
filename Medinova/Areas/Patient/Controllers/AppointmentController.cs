using Medinova.DTOs;
using Medinova.DTOs.AppointmentDtos;
using Medinova.Enums;
using Medinova.Models;
using Medinova.Services.AppointmentService;
using Medinova.Services.MailService;
using Medinova.Services.PatientService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Patient.Controllers
{
    public class AppointmentController : BaseController
    {
        MedinovaContext context = new MedinovaContext();

        private readonly IPatientService _patientService;

        private readonly IAppointmentService _appointmentService;
        private readonly IMailLogService _mailService;
        
        public AppointmentController()
        {
            _mailService = new MailLogService();    
            _appointmentService = new AppointmentService();
            _patientService = new PatientService();
        }

        public async Task<ActionResult> Index()
        {
            var values = await _patientService.GetListAppointmentByPatientIdAsync(PatientId);
            return View(values);
        }

        public async Task<ActionResult> ActiveAppointment()
        {
            var values = await _patientService.GetListAppointmentByPatientIdAsync(PatientId);
            return View("Index", values.Where(x => x.IsActive == true).Where(x => x.IsCompleted == false).ToList());
        }

        public async Task<ActionResult> PassiveAppointment()
        {
            var values = await _patientService.GetListAppointmentByPatientIdAsync(PatientId);
            return View("Index", values.Where(x => x.IsActive == false).Where(x => x.IsCompleted == false).ToList());
        }

        public async Task<ActionResult> IsCompletedAppointment()
        {
            var values = await _patientService.GetListAppointmentByPatientIdAsync(PatientId);
            return View("Index", values.Where(x => x.IsActive == false).Where(x => x.IsCompleted == true).ToList());
        }


        public async Task<ActionResult> AppointmentDetail(int appointmentId)
        {
            var value = await _patientService.GetAppointmentByPatientIdAsync(PatientId, appointmentId);
            return View(value);
        }

      
        
        [HttpPost]
        public async Task<ActionResult> CanceledAppointment(GetAppointmentInfoDto dto)
        {
           
            await _patientService.CanceledAppointmentByPatientIdAsync(dto);

            return RedirectToAction("AppointmentDetail",
                new { appointmentId = dto.AppointmentId });
        }

        public void DepartmentListItem()
        {

            var departments = context.Departments.ToList();

            ViewBag.departments = (from department in departments
                                   select new SelectListItem
                                   {
                                       Text = department.Name,
                                       Value = department.DepartmentId.ToString(),
                                   }).ToList();
        }

        public JsonResult GetDoctorsByDepartmentId(int departmentId)
        {
            var doctors = context.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => new
                {
                    Id = d.DoctorId,
                    Name = d.User.FirstName + " " + d.User.LastName
                })
                .ToList();

            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        public void DateListItem()
        {

            var dateList = new List<SelectListItem>();

            for (int i = 1; i < 10; i++)
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
       

        [HttpGet]
        public JsonResult GetAvailableHours(DateTime selectedDate, int doctorId)
        {
            var bookedTimes = context.Appointments
                    .Where(x => x.DoctorId == doctorId
                        && DbFunctions.TruncateTime(x.AppointmentDate)
                           == DbFunctions.TruncateTime(selectedDate))
                    .Select(x => x.AppointmentTime)
                    .ToList();
            var dtoList = new List<AppointmentAvailabilityDto>();

            foreach (var hour in Times.AppointmentHour)
            {
                var dto = new AppointmentAvailabilityDto();
                dto.Time = hour;

                if (bookedTimes.Contains(hour))
                {
                    dto.IsBooked = true;
                }
                else
                {
                    dto.IsBooked = false;
                }

                dtoList.Add(dto);
            }
            return Json(dtoList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateAppointment()
        {
            DepartmentListItem();
            DateListItem();
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> CreateAppointment(CreateAppointmentDto dto)
        {

            if (!ModelState.IsValid)
            {
                DepartmentListItem();
                DateListItem();
                return View(dto);
            }

            try
            {
                await _appointmentService.CreateAppointmentByPatientAsync(dto, PatientId);

                TempData["Success"] = "Randevu başarıyla oluşturuldu.";
                return RedirectToAction("Index", "Home", new { area = "Patient" });

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                DepartmentListItem();
                DateListItem();
                return View(dto);
            }

        }

    }
}