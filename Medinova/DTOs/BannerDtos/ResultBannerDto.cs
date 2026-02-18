using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Medinova.DTOs.BannerDtos
{
    public class ResultBannerDto
    {
        public int BannerId { get; set; }

        [Required(ErrorMessage ="Başlık alanı boş bırakılamaz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Görsel alanı boş bırakılamaz")]
        public string ImageUrl { get; set; }
    }
}