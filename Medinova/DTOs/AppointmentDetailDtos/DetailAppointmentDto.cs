using System;

namespace Medinova.DTOs.AppointmentDetailDtos
{
    public class DetailAppointmentDto
    {
        public int DoctorId { get; set; }
        public string DoctorTitle { get; set; }
        public string DoctorName { get; set; }
        public string DoctorLastName { get; set; }

        public int PatientId { get; set; }
        public string PatientTC { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }

        public string PatientGender { get; set; }
        public string BloodType { get; set; }
        public string PatientImage { get; set; }
        public DateTime PatientBirhtDate { get; set; }
        public int PatientAge { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientEmail { get; set; }

        public int AppointmentId { get; set; }
        public int AppointmentDetailId { get; set; }
        public string AppointmentDepartment { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string DoctorNote { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }


    }
}