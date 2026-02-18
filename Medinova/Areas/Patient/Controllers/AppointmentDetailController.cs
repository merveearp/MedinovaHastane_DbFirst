using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.Services.AppointmentDetailService;
using Medinova.Services.DoctorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Patient.Controllers
{
    public class AppointmentDetailController : BaseController
    {
        private readonly IAppointmentDetailService _appointmentDetailService;
        private readonly IDoctorService _doctorService;
        public AppointmentDetailController()
        {
            _appointmentDetailService = new AppointmentDetailService(); 
            _doctorService = new DoctorService();
        }
        public async Task<ActionResult> Index(int appointmentId, int appointmentDetailId)
        {
            var dto = new DetailAppointmentDto
            {
                AppointmentId = appointmentId,
                AppointmentDetailId = appointmentDetailId
            };
            var value = await _doctorService.GetAppointmentDetailAsync(appointmentId, appointmentDetailId);
            if (value == null)
            {
                return View(new DetailAppointmentDto());
            }
            return View(value);
        }

        public async Task<ActionResult> AppointmentDetailList()
        {
            var values = await _appointmentDetailService.GetAllAppointmentDetailAsync();
            return View(values);
        }
    }
}