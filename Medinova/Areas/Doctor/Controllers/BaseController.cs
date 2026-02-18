using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Doctor.Controllers
{
    public class BaseController : Controller
    {
        protected int DoctorId
        {
            get
            {
                if (Session["DoctorId"] == null)
                {
                    throw new UnauthorizedAccessException();
                }
                

                return (int)Session["DoctorId"];
            }
        }
    }
}