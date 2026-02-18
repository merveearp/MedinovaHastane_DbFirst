using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.Repositories.AppointmentDetailRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.AppointmentDetailService
{
    public class AppointmentDetailService : IAppointmentDetailService
    {
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        public AppointmentDetailService()
        {
            _appointmentDetailRepository = new AppointmentDetailRepository();
        }
        public async Task<List<ResultAppointmentDetailDto>> GetAllAppointmentDetailAsync()
        {
            var values = await _appointmentDetailRepository.GetAllAppointmentDetailAsync();
            return values.Select(x=> new ResultAppointmentDetailDto
            {
                AppointmentId = x.AppointmentId,
                AppointmentDetailId = x.AppointmentDetailId,
                AppointmentDate=x.Appointment.AppointmentDate,
                AppointmentDepartment=x.Appointment.Doctor.Department.Name,
                Diognasis=x.Diagnosis,
                Treatment=x.Treatment,
                DoctorNote=x.DoctorNote

            }).ToList();

        }
    }
}