using Medinova.ML;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        MedinovaContext context = new MedinovaContext();
        public ActionResult Index()
        {

            var service = new AppointmentForecastService();
            var forecast = service.GetMonthlyForecast();

            ViewBag.Forecast = forecast;

            ViewBag.DepartmentCount = context.Departments.Count();
            ViewBag.TotalAppointment = context.Appointments.Count();
            ViewBag.DoctorCount = context.Doctors.Where(x=>x.User.IsActive).Count();
            ViewBag.AppointmentDetailCount = context.AppointmentDetails.Count();

            var agustosCount = context.Appointments
               .Count(x => x.AppointmentDate.Year == 2025 &&
                x.AppointmentDate.Month == 8);

            var eylulCount = context.Appointments
                .Count(x => x.AppointmentDate.Year == 2025 &&
                 x.AppointmentDate.Month == 9);

            var ekimCount = context.Appointments
                .Count(x => x.AppointmentDate.Year == 2025 &&
                 x.AppointmentDate.Month == 10);

            var kasimCount = context.Appointments
                .Count(x => x.AppointmentDate.Year == 2025 &&
                 x.AppointmentDate.Month == 11);

            var aralikCount = context.Appointments
                .Count(x => x.AppointmentDate.Year == 2025 &&
                            x.AppointmentDate.Month == 12);

            var ocakCount = context.Appointments
                .Count(x => x.AppointmentDate.Year == 2026 &&
                            x.AppointmentDate.Month == 1);

            var subatCount = context.Appointments
                .Count(x => x.AppointmentDate.Year == 2026 &&
                            x.AppointmentDate.Month == 2);

            ViewBag.Agustos = agustosCount;
            ViewBag.Eylul = eylulCount;
            ViewBag.Ekim = ekimCount;
            ViewBag.Kasim = kasimCount;
            ViewBag.Aralik = aralikCount;
            ViewBag.Ocak = ocakCount;
            ViewBag.Subat = subatCount;




            var currentMonthDate = DateTime.Today.Month;

            var doctorList = context.Doctors
                .Select(d => new
                {
                    DoctorName = d.User.FirstName + " " + d.User.LastName,

                    CompletedAppointmentCount = d.Appointments
                        .Count(a =>
                            a.IsActive == true &&
                            a.AppointmentDate.Month == currentMonthDate
                        )
                })
                .OrderByDescending(d => d.CompletedAppointmentCount)
                .Take(10)
                .ToList();

            ViewBag.DoctorList = doctorList.Select(x => x.DoctorName).ToList();
            ViewBag.AppointmentByList = doctorList.Select(x => x.CompletedAppointmentCount).ToList();

            ViewBag.PreviousMonth = DateTime.Today
                .ToString("MMMM", new System.Globalization.CultureInfo("tr-TR"));


            ViewBag.cancel = context.Appointments.Count(x => x.IsActive == false && x.IsCompleted == false);
            ViewBag.active = context.Appointments.Count(x => x.IsActive == true && x.IsCompleted == false);
            ViewBag.complete = context.Appointments.Count(x => x.IsActive == false && x.IsCompleted == true);

            var departments = context.Appointments
                   .Where(a => a.IsCompleted)
                   .GroupBy(a => a.Doctor.Department.Name)
                   .Select(g => new
                   {
                       DepartmentName = g.Key,
                       AppointmentCount = g.Count()
                   })
                   .OrderByDescending(x => x.AppointmentCount)
                   .ToList();
            ViewBag.DepartmentNames = departments.Select(x => x.DepartmentName).ToList();
            ViewBag.DepartmentCounts = departments.Select(x => x.AppointmentCount).ToList();

            var doctorDepartments = context.Doctors
                .GroupBy(x => x.Department.Name)
                .Select(y => new
                {
                    DepartmentName = y.Key,
                    DoctorCount = y.Count()
                }).OrderByDescending(x => x.DoctorCount)
                .ToList();

            ViewBag.DoctorDepartment = doctorDepartments.Select(x => x.DepartmentName).ToList();
            ViewBag.DoctorDepartmentCount = doctorDepartments.Select(x => x.DoctorCount).ToList();




            var departments2 = context.Departments
                     .Where(d => d.IsActive)
                     .Select(d => new
                     {
                         DepartmentName = d.Name,
                         AppointmentCount = context.Appointments
                             .Count(a => a.Doctor.DepartmentId == d.DepartmentId
                                      && a.IsCompleted == true
                                      && a.AppointmentDate.Month == DateTime.Today.Month
                                      && a.IsActive == false)
                     })
                     .OrderByDescending(x => x.AppointmentCount)
                     .ToList();

            ViewBag.DepartmentNames2 = departments2.Select(x => x.DepartmentName).ToList();
            ViewBag.DepartmentCounts2 = departments2.Select(x => x.AppointmentCount).ToList();




            return View();
        }
    }
}