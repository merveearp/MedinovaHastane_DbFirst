using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medinova.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "İsim alanı zorunludur")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim alanı zorunludur")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="TC Kimlik NO zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Bilgilerin iletilmesi için Email alanı zorunludur")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bilgilerin iletilmesi için Telefon alanı zorunludur")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar zorunludur")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Doğum bilgisi zorunludur")]
        public DateTime BirthDate { get; set; }

    }
}