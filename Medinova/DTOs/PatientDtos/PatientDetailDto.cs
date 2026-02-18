using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.PatientDtos
{
    public class PatientDetailDto
    {
        public int PatientId { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string BloodType { get; set; }
        public string GenderName { get; set; }
    }
}