using Medinova.DTOs.DepartmentDtos;
using Medinova.Models;
using Medinova.Services.DepartmentService;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;


        public DepartmentController()
        {
            _departmentService = new DepartmentService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _departmentService.GetAllAsync();

            foreach (var item in values)
            {
                item.DoctorCount = await _departmentService
                    .GetCountDoctorsByDepartmentIdAsync(item.DepartmentId);
            }

            return View(values);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateDepartmentDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            await _departmentService.CreateAsync(createDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            await _departmentService.GetByIdAsync(id);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateDepartmentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }

            await _departmentService.UpdateAsync(updateDto);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _departmentService.DeleteAsync(id);
                TempData["Success"] = "Departman başarıyla pasife alındı.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetDoctors(int departmentId)
        {
            var doctors = await _departmentService.GetDoctorsByDepartmentIdAsync(departmentId);
            ViewBag.DeparmentName = doctors.FirstOrDefault(x => x.DepartmentId == departmentId).DepartmentName;
            return View(doctors);
        }

        

    }
}