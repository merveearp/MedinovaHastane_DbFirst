using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medinova.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "TC Kimlik No zorunlu")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalı")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Şifre zorunlu")]
        public string Password { get; set; }
    }

}