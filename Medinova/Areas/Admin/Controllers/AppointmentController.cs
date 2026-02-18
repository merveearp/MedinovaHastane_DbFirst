using Medinova.DTOs.AppointmentDtos;
using Medinova.Enums;
using Medinova.Models;
using Medinova.Services.AppointmentService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class AppointmentController : Controller
    {
        MedinovaContext context = new MedinovaContext();

        private readonly IAppointmentService _appointmentService;

        public AppointmentController()
        {
            _appointmentService= new AppointmentService();

        }
        public async Task<ActionResult> Index()
        {
            var values = await _appointmentService.GetListAppointmentAsync();
            ViewBag.Appointment = context.Appointments.Count();
            return View(values);
        }

        public async Task<ActionResult> ActiveAppointment()
        {
            var values = await _appointmentService.GetListAppointmentAsync();
            ViewBag.ActiveAppointment = context.Appointments.Count(x => x.IsActive == true && x.IsCompleted == false);
            return View("Index", values.Where(x => x.IsActive == true).Where(x => x.IsCompleted == false).ToList());
        }

        public async Task<ActionResult> PassiveAppointment()
        {
            var values = await _appointmentService.GetListAppointmentAsync();
            ViewBag.PassiveAppointment = context.Appointments.Count( x=> x.IsActive == false && x.IsCompleted == false);
            return View("Index", values.Where(x => x.IsActive == false).Where(x => x.IsCompleted == false).ToList());

        }

        public async Task<ActionResult> IsCompletedAppointment()
        {
            var values = await _appointmentService.GetListAppointmentAsync();
            ViewBag.IsCompletedAppointment = context.Appointments.Count(x => x.IsActive == false && x.IsCompleted == true);
           
            return View("Index", values.Where(x => x.IsActive == false).Where(x => x.IsCompleted == true).ToList());
        }

        public async Task<ActionResult> TodayAppointment()
        {
            var values = await _appointmentService.GetListAppointmentAsync();
            ViewBag.AppointmentDate = DateTime.Today;
            ViewBag.TodayAppointment = values.Count(x => x.IsActive == true && x.AppointmentDate == DateTime.Today);
            return View("Index", values.Where(x => x.IsActive == true).Where(x => x.AppointmentDate == DateTime.Today).ToList());
        }

        public async Task<ActionResult> GetAppointmentModal(int id)
        {
            var value = await _appointmentService.GetAppointmentAsync(id);

            return PartialView("_AppointmentModalPartial", value);
        }

    }
}