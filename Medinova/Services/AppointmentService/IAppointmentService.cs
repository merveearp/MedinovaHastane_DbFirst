using Medinova.DTOs.AppointmentDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task CreateAppointmentAsync(CreateAppointmentDto dto);
        Task CreateAppointmentByPatientAsync(CreateAppointmentDto dto, int patientId);



        Task CreateAppointmentByDoctorAsync(CreateAppointmentDto dto,int doctorId);
        Task CreateAppointmentByDoctorByPatientAsync(CreateAppointmentDto dto,int doctorId,int patientId);

        Task<List<ResultAppointmentDto>> GetListAppointmentAsync();
        Task<GetAppointmentInfoDto> GetAppointmentAsync(int appointmentId);
    }
}
