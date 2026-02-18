using Medinova.DTOs.AboutItemDtos;
using Medinova.Services.AboutItemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace Medinova.Areas.Admin.Controllers
{
    public class AboutItemController : Controller
    {
        private readonly IAboutItemService _aboutItemService;
        public AboutItemController()
        {
            _aboutItemService = new AboutItemService();
        }
        public async Task<ActionResult> Index()
        {
            var values = await _aboutItemService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public ActionResult CreateAboutItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateAboutItemDto createDto )
        {
            if(!ModelState.IsValid)
            {
                return View(createDto);
            }
            await _aboutItemService.CreateAsync(createDto);
            return RedirectToAction("Index");


        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var value = await _aboutItemService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateAboutItemDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }
           
            await _aboutItemService.UpdateAsync(updateDto);
            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> Delete(int id)
        {
            await _aboutItemService.DeleteAsync(id);
            return RedirectToAction("Index");
        }


    }
}