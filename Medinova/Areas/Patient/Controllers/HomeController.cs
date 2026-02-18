using Medinova.DTOs;
using Medinova.Models;
using Medinova.Services.AIService;
using Medinova.Services.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Patient.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        MedinovaContext context = new MedinovaContext();

        private readonly IPatientService _patientService;
        private readonly IAIService _aIService;
        public HomeController()
        {
            _patientService= new PatientService();
            _aIService= new AIService();
        }

        public async Task<ActionResult> Index()
        {

            ViewBag.CreatedUserDate = context.Patients.Where(x => x.PatientId == PatientId).Select(x => x.User.CreatedDate).FirstOrDefault();

            ViewBag.TotalAppointment = context.Appointments
                .Where(x => x.PatientId == PatientId)
                .Select(X => X.AppointmentId)
                .Count();

            ViewBag.ActiveAppointmentCount = context.Appointments
                .Where(x => x.PatientId == PatientId
                && x.IsActive == true
                && x.IsCompleted == false
                ).Count();

            ViewBag.CompletedAppointmentCount = context.Appointments
                .Where(x => x.PatientId == PatientId
                && x.IsActive == false
                && x.IsCompleted == true
                ).Count();

            ViewBag.CanceledAppointmentCount = context.Appointments
               .Where(x => x.PatientId == PatientId
               && x.IsActive == false
               && x.IsCompleted == false
               ).Count();

            ViewBag.AppointmentDate = DateTime.Today.ToShortDateString();


            var values = await _patientService
                 .GetListAppointmentByPatientIdAsync(PatientId);

            var todayAppointment = values
                .Where(x => x.AppointmentDate == DateTime.Today)
                .ToList();

           
            var model = new AIAppointmentViewDto
            {
                Appointments = todayAppointment,
                AIResponse = null 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AskAI(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                return Json(new { Answer = "Lütfen soru giriniz.", Department = (string)null });

            var response = await _aIService.AskAIAsync(question);

            return Json(response);
        }

    }
}


