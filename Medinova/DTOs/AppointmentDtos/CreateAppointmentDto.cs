using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AppointmentDtos
{
    public class CreateAppointmentDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime CreatedDate { get; set; }

        
        public string AppointmentTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
    }
}