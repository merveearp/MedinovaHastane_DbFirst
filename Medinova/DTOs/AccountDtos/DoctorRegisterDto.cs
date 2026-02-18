
using System;
using System.ComponentModel.DataAnnotations;


namespace Medinova.DTOs.AccountDtos
{
    public class DoctorRegisterDto
    {
        [Required(ErrorMessage = "İsim alanı zorunludur")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim alanı zorunludur")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "TC Kimlik NO zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        public string IdentityNumber { get; set; }    

        [Required(ErrorMessage = "Bilgilerin iletilmesi için Telefon alanı zorunludur")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen cinsiyet bilgisi giriniz")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Branş bilgisi zorunludur")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Kaydı yapılcak olan Yeni Doktorun derece bilgisi zorunludur")]
        public string Title { get; set; }
        public string BloodType { get; set; }

        [Required(ErrorMessage = "Kaydı yapılcak olan Yeni Doktorun yaş bilgisi için zorunludur")]
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
    }
}