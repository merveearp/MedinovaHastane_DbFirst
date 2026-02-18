using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.PatientDtos
{
    public class PatientEditDto
    {
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public int ActiveAppointmentCount { get; set; }

    }
}