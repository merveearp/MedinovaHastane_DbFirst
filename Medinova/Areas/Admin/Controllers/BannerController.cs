using Medinova.DTOs.BannerDtos;
using Medinova.Services.BannerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public BannerController()
        {
            _bannerService = new BannerService();
        }
        public async Task<ActionResult> Index()
        {
           var value =  await _bannerService.GetAsync();
            return View(value);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateBanner()
        {
            var value = await _bannerService.GetAsync();
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBanner(ResultBannerDto bannerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(bannerDto);
            }
        
            _bannerService.UpdateAsync(bannerDto);
            return RedirectToAction("Index");
        }
    }
}