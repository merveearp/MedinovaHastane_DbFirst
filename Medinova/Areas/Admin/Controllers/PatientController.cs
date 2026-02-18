using Medinova.DTOs.DoctorDtos;
using Medinova.DTOs.PatientDtos;
using Medinova.Models;
using Medinova.Services.DoctorService;
using Medinova.Services.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class PatientController : Controller
    {
    
        MedinovaContext context = new MedinovaContext();

        private readonly IPatientService _patientService;

        public PatientController()
        {
            _patientService = new PatientService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _patientService.GetAllAsync();
            return View(values);
        }

        public async Task<ActionResult> ActivePatients()
        {
            var values = await _patientService.GetAllAsync();
            return View("Index", values.Where(x => x.IsActive == true).ToList());
        }

        public async Task<ActionResult> PassivePatients()
        {
            var values = await _patientService.GetAllAsync();
            return View("Index", values.Where(x => x.IsActive == false).ToList());
        }

        [HttpGet]
        public async Task<ActionResult> PatientDetail(int id)
        {
            var value = await _patientService.GetDetailByIdAsync(id);
            return View(value);
        }


        [HttpGet]
        public ActionResult PatientEdit(int id)
        {
            var patient = context.Patients.FirstOrDefault(x => x.PatientId == id);


            var today = DateTime.Today;

            var activeAppointmentCount = context.Appointments.Count(x =>
                x.DoctorId == id &&
                x.IsCompleted == false &&
                x.IsActive == true &&
                x.AppointmentDate >= today);

            var dto = new PatientEditDto
            {
                PatientId = patient.PatientId,
                UserId = patient.UserId,
                IsActive = patient.User.IsActive,
                ImageUrl = patient.User.ImageUrl,
                FirstName = patient.User.FirstName,
                LastName = patient.User.LastName,
                ActiveAppointmentCount = activeAppointmentCount 
            };


            return View(dto);
        }


        [HttpPost]
        public ActionResult PatientEdit(PatientEditDto patientDto)
        {
            var patient = context.Patients.FirstOrDefault(x => x.PatientId == patientDto.PatientId);

            if (patient.User.IsActive && !patientDto.IsActive)
            {
                var today = DateTime.Today;

                var appointments = context.Appointments
                    .Where(x =>
                        x.PatientId == patient.PatientId &&
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

            patient.User.IsActive = patientDto.IsActive;

            context.SaveChanges();

            return RedirectToAction("PatientEdit", new { id = patientDto.PatientId });
        }

        public async Task<ActionResult> GetListAppointment(int id)
        {
            var values = await _patientService.GetListAppointmentByPatientIdAsync(id);

            ViewBag.PatientId = id;

            var patientInfo = context.Patients
                .Where(x => x.PatientId == id)
                .Select(x => new
                {
                    x.User.FirstName,
                    x.User.LastName,
                    x.User.ImageUrl
                })
                .FirstOrDefault();

            if (patientInfo != null)
            {
                ViewBag.PatientName =
                    $" {patientInfo.FirstName} {patientInfo.LastName}";

                ViewBag.PatientProfile = patientInfo.ImageUrl;
            }
            else
            {
                ViewBag.PatientName = "Hasta Bilgisi Bulunamadı";
                ViewBag.PatientProfile = "/Templates/medinova-1.0.0/img/default-profile.png";
            }

            return View(values);
        }

        public async Task<ActionResult> GetAppointmentByPatientIdAsync(int patientId, int appointmentId)
        {
            var value = await _patientService.GetAppointmentByPatientIdAsync(patientId, appointmentId);
            ViewBag.appointmentId = appointmentId;
            return View(value);
        }

        public async Task<ActionResult> GetAppointmentDetailByPatientAsync(int appointmentId, bool isCompleted)
        {
            ViewBag.AppointmentId = appointmentId;
            ViewBag.IsCompleted = isCompleted;
            var value = await _patientService.GetAppointmentDetailByPatientAsync(appointmentId, isCompleted);
            ViewBag.PatientId = value.PatientId;
            return View(value);
        }
   
        
    
    }
}