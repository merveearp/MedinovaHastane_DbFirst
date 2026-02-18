using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.DTOs.AppointmentDtos;
using Medinova.DTOs.DoctorDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.DoctorService
{
    public interface IDoctorService 
    {
        Task<List<ResultDoctorDto>> GetAllAsync();
        Task<DoctorDetailDto> GetDetailByIdAsync(int id);
        Task<List<ResultAppointmentDto>> GetListAppointmentByDoctorIdAsync(int doctorId);
        Task<List<ResultDoctorDto>> GetDoctorsByStatusAsync(bool isActive);

        Task<GetAppointmentInfoDto> GetAppointmentByDoctorIdAsync(int doctorId, int appointmentId);

        Task<DetailAppointmentDto> GetAppointmentDetailByDoctorAsync( int appointmentId, bool isCompleted);

        Task UpdateAppointmentByDoctorIdAsync(GetAppointmentInfoDto dto);
        Task CanceledAppointmentByDoctorIdAsync(GetAppointmentInfoDto dto);

        Task CreateAppointmentDetailAsync(CreateAppointmentDetailDto dto);
        Task UpdateAppointmentDetailAsync(DetailAppointmentDto dto);
        Task<DetailAppointmentDto> GetAppointmentDetailAsync(int appointmentId, int appointmentDetailId);



    }
}
