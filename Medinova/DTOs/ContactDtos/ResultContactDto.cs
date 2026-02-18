using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.ContactDtos
{
    public class ResultContactDto
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage ="Kullanıcı yönlendirici bilgi mesajı alanı doldurulması zorunludur")]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "Adres alanı boş bırakılamaz , zorunludur")]
        public string Address { get; set; }

        [Required(ErrorMessage = "İletişim alanı boş bırakılamaz , zorunludur")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mail alanı boş bırakılamaz , zorunludur")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Çalışma saati bilgisi boş bırakılamaz , zorunludur")]
        public string WorkingHours { get; set; }
    }
}