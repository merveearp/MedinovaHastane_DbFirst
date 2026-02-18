using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AppointmentDetailDtos
{
    public class ResultAppointmentDetailDto
    {
        public int AppointmentDetailId { get; set; }      
        public int AppointmentId { get; set; }
        public string AppointmentDepartment { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Diognasis { get; set; }
        public string Treatment { get; set; }
        public string DoctorNote { get; set; }
    }
}