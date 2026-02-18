using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AppointmentDetailDtos
{
    public class CreateAppointmentDetailDto
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage ="Randevu detay için hasta tanısı eklemelisiniz ")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "Randevu detay için hasta için tedavi eklemelisiniz ")]
        public string Treatment { get; set; }

        [Required(ErrorMessage = "Randevu detay için hasta için not eklemelisiniz ")]
        public string DoctorNote { get; set; }
    }
}