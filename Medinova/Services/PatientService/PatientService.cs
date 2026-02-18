using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.DTOs.AppointmentDtos;
using Medinova.DTOs.PatientDtos;
using Medinova.Models;
using Medinova.Repositories.PatientRepository;
using Medinova.Services.MailService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMailLogService _mailService;
        MedinovaContext context = new MedinovaContext();
        public PatientService()
        {
            _mailService = new MailLogService();
            _patientRepository = new PatientRepository();
        }
        public async Task<List<ResultPatientDto>> GetAllAsync()
        {
            var values = await _patientRepository.GetAllAsync();
            return values.Select(x => new ResultPatientDto
            {
                PatientId = x.PatientId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email,
                PhoneNumber = x.User.PhoneNumber,
                ImageUrl = x.User.ImageUrl,
                GenderId = x.User.Gender.GenderId,
                IsActive = x.User.IsActive

            }).ToList();
        }

        public async Task<PatientDetailDto> GetDetailByIdAsync(int id)
        {
            var value = await _patientRepository.GetDetailByIdAsync(id);
            var today = DateTime.Now;
            var birthDate = value.User.BirthDate.Value;
            var age = today.Year - birthDate.Year;
            return new PatientDetailDto
            {
                PatientId = value.PatientId,
                IdentityNumber = value.User.IdentityNumber,
                FirstName = value.User.FirstName,
                LastName = value.User.LastName,
                Email = value.User.Email,
                PhoneNumber = value.User.PhoneNumber,
                ImageUrl = value.User.ImageUrl,
                IsActive = value.User.IsActive,
                Age = age,
                BirthDate = birthDate,
                CreatedDate = value.User.CreatedDate,
                BloodType = value.User.BloodType,
                GenderName = value.User.Gender.GenderName
            };
        }

        public async Task<GetAppointmentInfoDto> GetAppointmentByPatientIdAsync(int patientId, int appointmentId)
        {
            var values = await _patientRepository.GetAppointmentByPatientIdAsync(patientId, appointmentId);

            var today = DateTime.Now;
            var birthDate = values.Patient.User.BirthDate.Value;
            var age = today.Year - birthDate.Year;
            return new GetAppointmentInfoDto
            {
                DoctorId = values.DoctorId,
                AppointmentId = appointmentId,
                DoctorName = values.Doctor.User.FirstName,
                DoctorLastName = values.Doctor.User.LastName,
                DoctorImage = values.Doctor.User.ImageUrl,
                DoctorBrans = values.Doctor.Department.Name,
                DoctorEmail = values.Doctor.User.Email,
                DoctorPhoneNumber = values.Doctor.User.PhoneNumber,
                DoctorTitle = values.Doctor.Title,
                DoctorGender = values.Doctor.User.Gender.GenderName,

                PatientId = patientId,
                PatientTC = values.Patient.User.IdentityNumber,
                PatientImage = values.Patient.User.ImageUrl,
                PatientName = values.Patient.User.FirstName,
                PatientLastName = values.Patient.User.LastName,
                PatientBrans = values.Doctor.Department.Name,
                PatientGender = values.Patient.User.Gender.GenderName,
                BloodType = values.Doctor.User.BloodType,
                PatientBirhtDate = (DateTime)values.Patient.User.BirthDate,
                PatientAge = age,
                PatientEmail = values.Patient.User.Email,
                PatientPhoneNumber = values.Patient.User.PhoneNumber,

                IsActive =values.IsActive,
                IsCompleted = values.IsCompleted,
                AppointmentDate = values.AppointmentDate,
                AppointmentTime = values.AppointmentTime,
                AppointmentDetailId = values.AppointmentDetails.Select(x => x.AppointmentDetailId).FirstOrDefault()
            };
        }

        public async Task<DetailAppointmentDto> GetAppointmentDetailByPatientAsync(int appointmentId, bool isCompleted)
        {
            var values = await _patientRepository.GetAppointmentDetailByPatientAsync(appointmentId, isCompleted);
            var today = DateTime.Now;
            var birthDate = values.Appointment.Patient.User.BirthDate.Value;
            var age = today.Year - birthDate.Year;
            return new DetailAppointmentDto
            {
                DoctorId = values.Appointment.DoctorId,
                DoctorTitle = values.Appointment.Doctor.Title,
                AppointmentId = appointmentId,
                DoctorName = values.Appointment.Doctor.User.FirstName,
                DoctorLastName = values.Appointment.Doctor.User.LastName,

                PatientId = values.Appointment.PatientId,
                PatientTC = values.Appointment.Patient.User.IdentityNumber,
                PatientImage = values.Appointment.Patient.User.ImageUrl,
                PatientName = values.Appointment.Patient.User.FirstName,
                PatientLastName = values.Appointment.Patient.User.LastName,
                PatientGender = values.Appointment.Patient.User.Gender.GenderName,
                BloodType = values.Appointment.Doctor.User.BloodType,
                PatientBirhtDate = (DateTime)values.Appointment.Patient.User.BirthDate,
                PatientAge = age,
                PatientEmail = values.Appointment.Patient.User.Email,
                PatientPhoneNumber = values.Appointment.Patient.User.PhoneNumber,

                IsCompleted = values.Appointment.IsCompleted,
                AppointmentDate = values.Appointment.AppointmentDate,
                AppointmentTime = values.Appointment.AppointmentTime,
                AppointmentDepartment = values.Appointment.Doctor.Department.Name,
                Diagnosis = values.Diagnosis,
                Treatment = values.Treatment,
                DoctorNote = values.DoctorNote
            };
        }

        public async Task<List<ResultAppointmentDto>> GetListAppointmentByPatientIdAsync(int patientId)
        {
            var values = await _patientRepository.GetListAppointmentByPatientIdAsync(patientId);

            return values.Select(x =>
            {

                var dto = new ResultAppointmentDto
                {


                    AppointmentId = x.AppointmentId,
                    DoctorId = x.DoctorId,
                    DoctorTitle = x.Doctor.Title,
                    DoctorName = x.Doctor.User.FirstName,
                    DoctorLastName = x.Doctor.User.LastName,
                    DoctorBranchName = x.Doctor.Department.Name,
                    PatientId = x.PatientId,
                    PatientName = x.Patient.User.FirstName,
                    PatientLastName = x.Patient.User.LastName,
                    AppointmentDate = x.AppointmentDate,
                    IsActive = x.IsActive,
                    AppointmentTime = x.AppointmentTime,
                    IsCompleted = x.IsCompleted

                };

                if (x.IsActive == true && x.IsCompleted == false)
                {
                    dto.StatusText = "Yaklaşan";
                    dto.BadgeClass = "bg-primary";
                    dto.IconClass = "fa-clock text-warning";
                    dto.IconTitle = "Gelecek Randevu";
                    dto.CompletedText = "Gelecek Randevu";
                }
                else if (x.IsActive == false && x.IsCompleted == true)
                {
                    dto.StatusText = "Tamamlandı";
                    dto.BadgeClass = "bg-secondary";
                    dto.IconClass = "fa-check-circle text-success";
                    dto.IconTitle = "Geçmiş Randevu";
                    dto.CompletedText = "Geçmiş Randevu";
                }
                else if (x.IsActive == false && x.IsCompleted == false)
                {
                    dto.StatusText = "İptal Edildi";
                    dto.BadgeClass = "bg-danger";
                    dto.IconClass = "fa-times-circle text-danger";
                    dto.IconTitle = "İptal Edilen Randevu";
                    dto.CompletedText = "İptal Edilen Randevu";
                }

                return dto;

            }).ToList();
        }

        public async Task<List<ResultPatientDto>> GetAllByDoctorIdAsync(int doctorId)
        {
            var values = await _patientRepository.GetAllByDoctorIdAsync(doctorId);

            return values.Select(x => new ResultPatientDto
            {

                PatientId = x.PatientId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email,
                PhoneNumber = x.User.PhoneNumber,
                ImageUrl = x.User.ImageUrl,
                GenderId = x.User.Gender.GenderId,
                IsActive = x.User.IsActive
            }).ToList();
        }

        public async Task<List<ResultAppointmentDto>> GetListAppointmentByPatientIdByDoctorIdAsync(int patientId, int doctorId)
        {
            var values = await _patientRepository.GetListAppointmentByPatientIdDoctorIdAsync(patientId, doctorId);

            return values.Select(x =>
            {

                var dto = new ResultAppointmentDto
                {

                    AppointmentId = x.AppointmentId,
                    DoctorId = doctorId,
                    DoctorTitle = x.Doctor.Title,
                    DoctorImage=x.Doctor.User.ImageUrl,
                    DoctorName = x.Doctor.User.FirstName,
                    DoctorLastName = x.Doctor.User.LastName,
                    DoctorBranchName = x.Doctor.Department.Name,
                    PatientId = patientId,
                    PatientImage=x.Patient.User.ImageUrl,
                    PatientName = x.Patient.User.FirstName,
                    PatientLastName = x.Patient.User.LastName,
                    AppointmentDate = x.AppointmentDate,
                    IsActive = x.IsActive,
                    AppointmentTime = x.AppointmentTime,
                    IsCompleted = x.IsCompleted

                };

                if (x.IsActive == true && x.IsCompleted == false)
                {
                    dto.StatusText = "Yaklaşan";
                    dto.BadgeClass = "bg-primary";
                    dto.IconClass = "fa-clock text-warning";
                    dto.IconTitle = "Gelecek Randevu";
                    dto.CompletedText = "Gelecek Randevu";
                }
                else if (x.IsActive == false && x.IsCompleted == true)
                {
                    dto.StatusText = "Tamamlandı";
                    dto.BadgeClass = "bg-secondary";
                    dto.IconClass = "fa-check-circle text-success";
                    dto.IconTitle = "Geçmiş Randevu";
                    dto.CompletedText = "Geçmiş Randevu";
                }
                else if (x.IsActive == false && x.IsCompleted == false)
                {
                    dto.StatusText = "İptal Edildi";
                    dto.BadgeClass = "bg-danger";
                    dto.IconClass = "fa-times-circle text-danger";
                    dto.IconTitle = "İptal Edilen Randevu";
                    dto.CompletedText = "İptal Edilen Randevu";
                }

                return dto;
            }).ToList();
        }

        public async Task UpdateAppointmentByPatientIdAsync(GetAppointmentInfoDto dto)
        {
            var value = await _patientRepository.GetAppointmentByPatientIdAsync(dto.PatientId, dto.AppointmentId);

            value.IsActive = dto.IsActive;

            await _patientRepository.UpdateAppointmentByPatientIdAsync(value);
        }

        public async Task CanceledAppointmentByPatientIdAsync(GetAppointmentInfoDto dto)
        {
            var appointment = await _patientRepository
                .GetAppointmentByPatientIdAsync(dto.PatientId, dto.AppointmentId);

            if (appointment == null)
                throw new Exception("Randevu bulunamadı.");

            appointment.IsActive = false;
            appointment.IsCompleted = false;

            await _patientRepository.UpdateAppointmentByPatientIdAsync(appointment);

            var patient = await context.Patients
                .Where(x => x.PatientId == appointment.PatientId)
                .Select(x => new
                {
                    x.User.Email,
                    FullName = x.User.FirstName + " " + x.User.LastName
                })
                .FirstOrDefaultAsync();

            var doctor = await context.Doctors
                .Where(x => x.DoctorId == appointment.DoctorId)
                .Select(x => new
                {
                    FullName = x.User.FirstName + " " + x.User.LastName,
                    DepartmentName = x.Department.Name
                })
                .FirstOrDefaultAsync();

            if (patient == null || doctor == null)
                throw new Exception("Mail bilgileri bulunamadı.");

            string mailBody = $@"
<div style='background-color:#f4f6f9; padding:30px; font-family:Segoe UI, Arial, sans-serif;'>

    <div style='max-width:600px; margin:auto; background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 5px 15px rgba(0,0,0,0.05);'>

        <!-- HEADER -->
        <div style='background-color:#0d6efd; padding:25px; text-align:center;'>

            <img src='https://i.hizliresim.com/nvqbi32.png' 
                 alt='Medinova Hastanesi' 
                 style='height:70px; margin-bottom:10px;' />

            <h2 style='margin:0; color:white;'>Medinova Hastanesi</h2>
            <p style='margin:5px 0 0 0; font-size:13px; color:white;'>Randevu Bildirimi</p>
        </div>

        <!-- CONTENT -->
        <div style='padding:30px;'>

            <h3 style='color:#dc3545; margin-top:0;'>Randevunuz İptal Edildi</h3>

            <p style='font-size:14px; color:#555;'>
                Aşağıdaki randevu kaydınız iptal edilmiştir:
            </p>

            <table style='width:100%; font-size:14px; margin-top:20px; border-collapse:collapse;'>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Tarih</b></td>
                    <td style='padding:8px;'>{appointment.AppointmentDate:dd.MM.yyyy}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Saat</b></td>
                    <td style='padding:8px;'>{appointment.AppointmentTime}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Doktor</b></td>
                    <td style='padding:8px;'>Dr. {doctor.FullName}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Bölüm</b></td>
                    <td style='padding:8px;'>{doctor.DepartmentName}</td>
                </tr>

            </table>

            <p style='margin-top:25px; font-size:14px; color:#777;'>
                İhtiyaç halinde tekrar randevu oluşturabilirsiniz.
            </p>

            <p style='font-size:14px;'>
                Sağlıklı günler dileriz.<br/>
                <b>Medinova Hastanesi</b>
            </p>

        </div>

                <!-- FOOTER -->
                <div style='background:#f1f1f1; padding:15px; text-align:center; font-size:12px; color:#888;'>
                    © {DateTime.Now.Year} Medinova Hastanesi – Tüm Hakları Saklıdır
                </div>

            </div>

        </div>
";

            await _mailService.SendMailAsync(
                    patient.Email,
                    "Medinova - Randevunuz İptal Edildi",
                    mailBody
                );

        }
    
    }
}