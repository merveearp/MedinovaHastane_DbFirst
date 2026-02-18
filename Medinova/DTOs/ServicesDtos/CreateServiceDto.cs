using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.ServicesDtos
{
    public class CreateServiceDto
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ServiceIcon { get; set; }
    }
}