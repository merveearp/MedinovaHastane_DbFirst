using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.DoctorDtos
{
    public class DoctorEditDto
    {

        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ActiveAppointmentCount { get; set; }
       
    }
}