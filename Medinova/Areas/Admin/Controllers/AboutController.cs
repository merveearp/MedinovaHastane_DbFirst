using Medinova.DTOs.AboutDtos;
using Medinova.Services.AboutService;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{   
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        public AboutController()
        {
            _aboutService = new AboutService();
            
        }

        public async Task<ActionResult> Index()
        {
            var value = await _aboutService.GetAsync();
            return View(value);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateAbout()
        {
            var value = await _aboutService.GetAsync();
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAbout(ResultAboutDto aboutDto)
        {
           if(!ModelState.IsValid)
            {
                return View(aboutDto);
            }

            await _aboutService.UpdateAsync(aboutDto);
           return RedirectToAction("Index");
        }


    }
}