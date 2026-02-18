using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.ProfileDtos
{
    public class DoctorProfileEditDto
    {
  
            public int DoctorId { get; set; }
            public int UserId { get; set; }

            [Required(ErrorMessage = "Unvan zorunludur.")]
            public string Title { get; set; }

            public string Description { get; set; }

            [Required(ErrorMessage = "Ad zorunludur.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Soyad zorunludur.")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Email zorunludur.")]
            [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Telefon zorunludur.")]
            public string PhoneNumber { get; set; }

            public DateTime? BirthDate { get; set; }

            public string BloodType { get; set; }

            public string ImageUrl { get; set; }
        }
    }
