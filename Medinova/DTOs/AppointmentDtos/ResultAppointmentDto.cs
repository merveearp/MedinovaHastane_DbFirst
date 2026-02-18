using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AppointmentDtos
{
    public class ResultAppointmentDto
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorImage { get; set; }
        public string DoctorTitle { get; set; }
        public string DoctorName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorBranchName { get; set; }

        public int PatientId { get; set; }
        public string PatientImage { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsActive { get; set; }
        public string AppointmentTime { get; set; }
        public bool IsCompleted { get; set; }


        public string StatusText { get; set; }
        public string CompletedText { get; set; }
        public string BadgeClass { get; set; }
        public string IconClass { get; set; }
        public string IconTitle { get; set; }


    }
}