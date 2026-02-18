using Medinova.DTOs.BlogDtos;
using Medinova.DTOs.MediaDtos;
using Medinova.Services.MediaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        public MediaController()
        {
            _mediaService = new MediaService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _mediaService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateMediaDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            await _mediaService.CreateAsync(createDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            await _mediaService.GetByIdAsync(id);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateMediaDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }

            await _mediaService.UpdateAsync(updateDto);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _mediaService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}