using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs.AppointmentDtos
{
    public class GetAppointmentInfoDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorTitle { get; set; }
        public string DoctorBrans { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorPhoneNumber { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorImage { get; set; }
        public string DoctorGender { get; set; }

        public int PatientId { get; set; }
        public string PatientTC { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientBrans { get; set; }
        public string PatientGender { get; set; }
        public string BloodType { get; set; }
        public string PatientImage { get; set; }
        public DateTime PatientBirhtDate { get; set; }
        public int PatientAge { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientEmail { get; set; }

        public int AppointmentId { get; set; }
        public int? AppointmentDetailId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }

        public bool IsActive { get; set; }

    }
}