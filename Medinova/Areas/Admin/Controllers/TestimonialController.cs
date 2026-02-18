using Medinova.DTOs.MediaDtos;
using Medinova.DTOs.TestimonialDtos;
using Medinova.Services.TestimonialService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;
        public TestimonialController()
        {
            _testimonialService = new TestimonialService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _testimonialService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateTestimonialDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            await _testimonialService.CreateAsync(createDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            await _testimonialService.GetByIdAsync(id);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateTestimonialDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }

            await _testimonialService.UpdateAsync(updateDto);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _testimonialService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}