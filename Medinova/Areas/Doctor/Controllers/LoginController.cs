using Medinova.DTOs;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Medinova.Areas.Doctor.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Panel",new { area = ""});
        }

    }
}