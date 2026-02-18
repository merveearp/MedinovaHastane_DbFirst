using Medinova.DTOs.ServicesDtos;
using Medinova.Services.ServiceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {
      private readonly IServiceService _serviceService;
        public ServiceController()
        {
            _serviceService = new ServiceService();
        }

        public async Task<ActionResult> Index()
        {
            var values = await _serviceService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateServiceDto createDto)
        {
            if(!ModelState.IsValid)
            {
                return View(createDto);
            }

            await _serviceService.CreateAsync(createDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var value = await _serviceService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateServiceDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }

            await _serviceService.UpdateAsync(updateDto);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}