using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AboutItemDtos
{
    public class CreateAboutItemDto
    {
        [Required(ErrorMessage ="İkon Alanı boş bırakılamaz!")]
        public string Icon { get; set; }

        [Required(ErrorMessage = "AltBaşlık Alanı boş bırakılamaz!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Başlık Alanı boş bırakılamaz!")]
        public string Name { get; set; }
    }
}