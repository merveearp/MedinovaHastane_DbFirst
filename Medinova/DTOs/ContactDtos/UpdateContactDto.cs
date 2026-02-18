using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.ContactDtos
{
    public class UpdateContactDto
    {
        public int ContactId { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
    }
}