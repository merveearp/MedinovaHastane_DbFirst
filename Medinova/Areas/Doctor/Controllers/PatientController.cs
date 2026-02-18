using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.DTOs.AppointmentDtos;
using Medinova.DTOs.PatientDtos;
using Medinova.Models;
using Medinova.Services.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Doctor.Controllers
{
    public class PatientController : BaseController
    {
        private readonly IPatientService _patientService;

        MedinovaContext context = new MedinovaContext();
        public PatientController()
        {
            _patientService = new PatientService();
        }


        public async Task<ActionResult> Index()
        {         
            
            var values = await _patientService.GetAllByDoctorIdAsync(DoctorId);
            ViewBag.DoctorId = DoctorId;
            return View(values);
        }

        public async Task<ActionResult> ActivePatients()
        {

            var values = await _patientService.GetAllByDoctorIdAsync(DoctorId); ;
            return View("Index", values.Where(x => x.IsActive == true).ToList());
        }

        public async Task<ActionResult> PassivePatients()
        {

            var values = await _patientService.GetAllByDoctorIdAsync(DoctorId); 
            return View("Index", values.Where(x => x.IsActive == false).ToList());
        }

        public async Task<ActionResult> PatientDetail(int id)
        {
            var value = await _patientService.GetDetailByIdAsync(id);
            ViewBag.DoctorId = DoctorId;

            var patient = context.Patients.Where(x => x.PatientId == id).FirstOrDefault();

            ViewBag.PatienImage = patient.User.ImageUrl;
            ViewBag.PatienName = patient.User.FirstName + " " + patient.User.LastName;
            ViewBag.PatienTC = patient.User.IdentityNumber;

            return View(value);
        }

        public async Task<ActionResult> GetListAppointment(int patientId)
        {

            var values = await _patientService.GetListAppointmentByPatientIdByDoctorIdAsync(patientId,DoctorId);

            ViewBag.DoctorId = DoctorId;
            ViewBag.PatientId= patientId;

            var patientInfo = context.Patients.Where(x => x.PatientId == patientId).Select(x => new
            {
                x.PatientId,
                x.User.FirstName,
                x.User.LastName,
                x.User.ImageUrl
            }).FirstOrDefault();

            ViewBag.PatientName = $" {patientInfo.FirstName} {patientInfo.LastName}";

            ViewBag.PatientProfile = patientInfo.ImageUrl;

            return View(values);
        }

        public async Task<ActionResult> GetListIsCanceledAppointment(int patientId)
        {

            var values = await _patientService.GetListAppointmentByPatientIdByDoctorIdAsync(patientId,DoctorId);

            ViewBag.DoctorId = DoctorId;
            ViewBag.PatientId = patientId;

            var patientInfo = context.Patients.Where(x => x.PatientId == patientId).Select(x => new
            {
                x.PatientId,
                x.User.FirstName,
                x.User.LastName,
                x.User.ImageUrl
            }).FirstOrDefault();

            ViewBag.PatientName = $" {patientInfo.FirstName} {patientInfo.LastName}";

            ViewBag.PatientProfile = patientInfo.ImageUrl;

            return View("GetListAppointment", values.Where(x => x.IsActive == false && x.IsCompleted==false).ToList());
        }

        public async Task<ActionResult> GetListIsCompletedAppointment(int patientId)
        {

            var values = await _patientService.GetListAppointmentByPatientIdByDoctorIdAsync(patientId, DoctorId);

            ViewBag.DoctorId = DoctorId;
            ViewBag.PatientId = patientId;

            var patientInfo = context.Patients.Where(x => x.PatientId == patientId).Select(x => new
            {
                x.PatientId,
                x.User.FirstName,
                x.User.LastName,
                x.User.ImageUrl
            }).FirstOrDefault();

            ViewBag.PatientName = $" {patientInfo.FirstName} {patientInfo.LastName}";

            ViewBag.PatientProfile = patientInfo.ImageUrl;

            return View("GetListAppointment", values.Where(x => x.IsActive == false && x.IsCompleted == true).ToList());
        }
        public async Task<ActionResult> GetListIsActiveAppointment(int patientId)
        {

            var values = await _patientService.GetListAppointmentByPatientIdByDoctorIdAsync(patientId, DoctorId);

            ViewBag.DoctorId = DoctorId;
            ViewBag.PatientId = patientId;

            var patientInfo = context.Patients.Where(x => x.PatientId == patientId).Select(x => new
            {
                x.PatientId,
                x.User.FirstName,
                x.User.LastName,
                x.User.ImageUrl
            }).FirstOrDefault();

            ViewBag.PatientName = $" {patientInfo.FirstName} {patientInfo.LastName}";

            ViewBag.PatientProfile = patientInfo.ImageUrl;

            return View("GetListAppointment", values.Where(x => x.IsActive == true && x.IsCompleted == false).ToList());
        }


    }
}