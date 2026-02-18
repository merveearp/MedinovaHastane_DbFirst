using Medinova.DTOs.ContactDtos;
using Medinova.Services.ContactService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController()
        {
            _contactService = new ContactService();
        }
        public async Task<ActionResult> Index()
        {
            var value = await _contactService.GetAsync();
            return View(value);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateContact()
        {
            var value = await _contactService.GetAsync();
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateContact(ResultContactDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return View (contactDto);
            }

            await _contactService.UpdateAsync(contactDto);
            return RedirectToAction("Index");
        }
    }
}