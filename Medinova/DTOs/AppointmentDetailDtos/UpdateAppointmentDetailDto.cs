using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AppointmentDetailDtos
{
    public class UpdateAppointmentDetailDto
    {
        public int AppointmentDetailId { get; set; }
        public int AppointentId { get; set; }
        public string Diognasis { get; set; }
        public string Treatment { get; set; }
        public string DoctorNote { get; set; }
    }
}