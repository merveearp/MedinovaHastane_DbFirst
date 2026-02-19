using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.Models;
using Medinova.Repositories.AppointmentRepository;
using Medinova.Services.DoctorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Doctor.Controllers
{
    public class AppointmentDetailController : BaseController
    {
        private readonly IDoctorService _doctorService;

        public AppointmentDetailController()
        {
            _doctorService=new DoctorService();
        }
        public async Task<ActionResult> Index(int appointmentId,int appointmentDetailId)
        {
            var dto = new DetailAppointmentDto
            {
                AppointmentId = appointmentId,
                AppointmentDetailId = appointmentDetailId
            };
            var value = await _doctorService.GetAppointmentDetailAsync(appointmentId, appointmentDetailId);
            return View(value);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int appointmentId, int appointmentDetailId)
        {
            var value = await _doctorService
                .GetAppointmentDetailAsync(appointmentId, appointmentDetailId);

            return View(value);
        }


        [HttpPost]
        public async Task<ActionResult> Update(DetailAppointmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _doctorService.UpdateAppointmentDetailAsync(dto);
            TempData["SuccessAppointment"] = "Muayene sonuçları başarıyla güncellendi.";
            return RedirectToAction("Index", "AppointmentDetail", new
            {
                area = "Doctor",
                appointmentId = dto.AppointmentId,
                appointmentDetailId = dto.AppointmentDetailId
            });
        }



        [HttpGet]
        public ActionResult Create(int appointmentId)
        {
            var dto = new CreateAppointmentDetailDto
            {
                AppointmentId = appointmentId
            };

            return View(dto);
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreateAppointmentDetailDto dto)
        {
         
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _doctorService.CreateAppointmentDetailAsync(dto);
            return RedirectToAction(
                 "AppointmentDetail",
                 "Appointment",
                 new { appointmentId = dto.AppointmentId }
             );


        }


    }
}