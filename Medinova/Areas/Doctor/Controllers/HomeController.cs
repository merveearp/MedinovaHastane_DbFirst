using Medinova.Models;
using Medinova.Services.AppointmentService;
using Medinova.Services.DoctorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Doctor.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly MedinovaContext context;
        private readonly IDoctorService _doctorService;

        public HomeController()
        {
            context = new MedinovaContext();
            _doctorService = new DoctorService();
        }
        public async Task<ActionResult> Index()
        {


            ViewBag.cancel = context.Appointments.Where(x => x.DoctorId == DoctorId).Count(x => x.IsActive == false && x.IsCompleted == false);
            ViewBag.active = context.Appointments.Where(x => x.DoctorId == DoctorId).Count(x => x.IsActive == true && x.IsCompleted == false);
            ViewBag.complete = context.Appointments.Where(x => x.DoctorId == DoctorId).Count(x => x.IsActive == false && x.IsCompleted == true);

            ViewBag.CreatedDoctorDate = context.Doctors.Where(x => x.DoctorId == DoctorId).Select(x => x.User.CreatedDate).FirstOrDefault();

            ViewBag.TotalAppointment = context.Appointments
                .Where(x => x.DoctorId == DoctorId)
                .Select(X => X.AppointmentId)
                .Count();

            ViewBag.TotalCountPatient = context.Appointments
               .Where(x => x.DoctorId == DoctorId && x.Patient.User.IsActive)
               .Select(x => x.PatientId)
               .Distinct()
               .Count();

            ViewBag.ActiveAppointmentCount = context.Appointments
                .Where(x => x.DoctorId == DoctorId
                && x.IsActive == true
                && x.IsCompleted == false
                ).Count();

            ViewBag.CompletedAppointmentCount = context.Appointments
                .Where(x => x.DoctorId == DoctorId
                && x.IsActive == false
                && x.IsCompleted == true
                ).Count();

            var values = await _doctorService.GetListAppointmentByDoctorIdAsync(DoctorId);

            var todayAppointments = values.Where(x => x.IsActive && x.AppointmentDate == DateTime.Today).OrderBy(x =>x.AppointmentTime).ToList();

            ViewBag.AppointmentDate = DateTime.Today.ToShortDateString();
             return View(todayAppointments);
        }

    }

}
