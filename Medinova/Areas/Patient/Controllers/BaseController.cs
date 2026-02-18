using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinova.Areas.Patient.Controllers
{
    public class BaseController : Controller
    {
        protected int PatientId
        {
            get
            { 
                if(Session["PatientId"] == null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                return (int)Session["PatientId"];

             }
        }
           
    }
}


