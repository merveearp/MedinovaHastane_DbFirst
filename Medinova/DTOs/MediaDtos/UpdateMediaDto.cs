using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.MediaDtos
{
    public class UpdateMediaDto
    {
        public int MediaId { get; set; }
        public string MediaName { get; set; }
        public string MediaUrl { get; set; }
        public string MediaIcon { get; set; }
    }
}