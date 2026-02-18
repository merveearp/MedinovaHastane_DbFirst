using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.DTOs.AppointmentDtos;
using Medinova.DTOs.PatientDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.PatientService
{
    public interface IPatientService
    {
        Task<List<ResultPatientDto>> GetAllAsync();
        Task<List<ResultPatientDto>> GetAllByDoctorIdAsync(int doctorId);
        Task<PatientDetailDto> GetDetailByIdAsync(int id);
        Task<List<ResultAppointmentDto>> GetListAppointmentByPatientIdAsync(int patientId);
        Task<GetAppointmentInfoDto> GetAppointmentByPatientIdAsync(int patientId, int appointmentId);
        Task<DetailAppointmentDto> GetAppointmentDetailByPatientAsync(int appointmentId, bool isCompleted);
        Task<List<ResultAppointmentDto>> GetListAppointmentByPatientIdByDoctorIdAsync(int patientId, int doctorId);
        Task UpdateAppointmentByPatientIdAsync(GetAppointmentInfoDto dto);

        Task CanceledAppointmentByPatientIdAsync(GetAppointmentInfoDto dto);
    }


}
