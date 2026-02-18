using Medinova.DTOs;
using Medinova.DTOs.AppointmentDtos;
using Medinova.DTOs.BlogDtos;
using Medinova.Enums;
using Medinova.Models;
using Medinova.Services.AIService;
using Medinova.Services.AppointmentService;
using Medinova.Services.BlogService;
using Medinova.Services.DoctorService;
using Medinova.Services.MailService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        MedinovaContext context = new MedinovaContext();

        private readonly IMailLogService mailService;
        private readonly IBlogService _blogService;
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        public DefaultController()
        {
            _appointmentService = new AppointmentService();
            _doctorService = new DoctorService();
            _blogService = new BlogService();
            mailService = new MailLogService();
        }


        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public PartialViewResult DefaultAppointment()
        {
            var departments = context.Departments.ToList();

            ViewBag.departments = (from department in departments
                                   select new SelectListItem
                                   {
                                       Text = department.Name,
                                       Value = department.DepartmentId.ToString(),
                                   }).ToList();

            var dateList = new List<SelectListItem>();

            for (int i = 1; i < 8; i++)
            {
                var date = DateTime.Now.AddDays(i);

                dateList.Add(new SelectListItem
                {
                    Text = date.ToString("dd.MMMM.dddd"),
                    Value = date.ToString("yyyy-MM-dd")
                });
            }
            ViewBag.dateList = dateList;

            return PartialView();
        }


        [HttpPost]
        public async Task<ActionResult> MakeAppointment(CreateAppointmentDto dto)
        {
            if (Session["PatientId"] == null)
            {
                TempData["LoginRequired"] = true;
                return RedirectToAction("Index", "Default");
            }

            int patientId = (int)Session["PatientId"];

            try
            {
                await _appointmentService.CreateAppointmentByPatientAsync(dto, patientId);

                TempData["AppointmentSuccess"] = "Randevunuz başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                TempData["AppointmentError"] = ex.Message;
            }

            return RedirectToAction("Index", "Default");
        }


        public JsonResult GetDoctorsByDepartmentId(int departmentId)
        {
            var doctors = context.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .Select(doctor => new SelectListItem
                {
                    Text = doctor.User.FirstName + " " + doctor.User.LastName,
                    Value = doctor.DoctorId.ToString(),
                }).ToList();

            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvailableHours(DateTime selectedDate, int doctorId)
        {
            var bookedTimes = context.Appointments.Where(x => x.DoctorId == doctorId && x.AppointmentDate == selectedDate).Select(x => x.AppointmentTime).ToList();

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
        
        public PartialViewResult DefaultHero()
        {
            var value = context.Banners.FirstOrDefault();
            return PartialView(value);
        }



        public PartialViewResult DefaultAbout()
        {
            var value = context.Abouts.FirstOrDefault();
            return PartialView(value);

        }
  
        public PartialViewResult DefaultAboutItem()
        {
            var values = context.AboutItems.ToList();
            return PartialView(values);
        }

    
        public PartialViewResult DefaultServices()
        {
            var values = context.Services.ToList();
            return PartialView(values);
        }

        [HttpGet]
        public PartialViewResult DefaultTeam()
        {
            var departments = context.Departments
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToList();

            return PartialView(departments);
        }


        [HttpGet]
        public PartialViewResult DefaultTestimonial()
        {
            var values = context.Testimonials.ToList();
            return PartialView(values);
        }

        [HttpGet]
        public PartialViewResult DefaultBlog()
        {
            var values = context.Blogs.OrderByDescending(x=>x.BlogId).ToList();
            return PartialView(values);
        }

        [HttpGet]
        public PartialViewResult HeadSection()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult FooterSection()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult ScriptsSection()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult MediaSection()
        {
            var values = context.Medias.ToList();
            return PartialView(values);
        }

        [HttpGet]
        public PartialViewResult ContactSection()
        {
            var value = context.Contacts.FirstOrDefault();
            return PartialView(value);
        }

        [HttpGet]
        public PartialViewResult FooterContactSection()
        {
            var value = context.Contacts.FirstOrDefault();
            return PartialView(value);
        }

        [HttpGet]
        public PartialViewResult FooterMediaSection()
        {
            var values = context.Medias.ToList();
            return PartialView(values);
        }

        [HttpGet]
        public PartialViewResult NavbarSection()
        {
            return PartialView();
        }

        public ActionResult Detail(int id)
        {
            var blog = context.Blogs
                .Where(x => x.BlogId == id)
                .Select(x => new UpdateBlogDto
                {
                    BlogId = x.BlogId,
                    BlogTitle = x.BlogTitle,
                    BlogSubtitle = x.BlogSubtitle,
                    BlogContent = x.BlogContent,
                    Image1 = x.Image1,
                    Image2 = x.Image2,
                    BlogWriter = x.BlogWriter,
                    WriterProfile = x.WriterProfile
                }).FirstOrDefault();

            return PartialView("_BlogDetail", blog);
        }





    }
}