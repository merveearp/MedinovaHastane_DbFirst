using Medinova.Models;
using Medinova.Services.AppointmentDetailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class AppointmentDetailController : Controller
    {
        private readonly IAppointmentDetailService _appointmentDetailService;
        MedinovaContext context = new MedinovaContext();
        public AppointmentDetailController()
        {
            _appointmentDetailService = new AppointmentDetailService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _appointmentDetailService.GetAllAppointmentDetailAsync();
            ViewBag.DetailCount = context.AppointmentDetails.Count();   
            return View(values);
        }
    }
}