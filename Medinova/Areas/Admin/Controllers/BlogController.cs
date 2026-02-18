using Medinova.DTOs.BlogDtos;
using Medinova.DTOs.ServicesDtos;
using Medinova.Services.BlogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController()
        {
            _blogService = new BlogService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _blogService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBlogDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            await _blogService.CreateAsync(createDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
           
            var value = await _blogService.GetByIdAsync(id);
            return View(value);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var value = await _blogService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateBlogDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }

            await _blogService.UpdateAsync(updateDto);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}