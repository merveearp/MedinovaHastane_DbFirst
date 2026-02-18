using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AboutDtos
{
    public class ResultAboutDto
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage ="Başlık alanı Boş bırakılamaz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama alanı Boş bırakılamaz")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Resim alanı Boş bırakılamaz")]
        public string ImageUrl { get; set; }
        
    }
}