using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Controllers
{
    public class _UILayoutController : Controller
    {
        MedinovaContext context = new MedinovaContext();
        public ActionResult Index()
        {
            return View();
        }

        
    }
}