using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.DoctorRepository
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor> GetDetailByIdAsync(int id);
        Task<List<Appointment>> GetListAppointmentByDoctorIdAsync(int doctorId);
        Task<Appointment> GetAppointmentByDoctorIdAsync(int doctorId, int appointmentId);
        Task<AppointmentDetail> GetAppointmentDetailByDoctorAsync(int appointmentId,bool isCompleted);


        Task UpdateAppointmentByDoctorIdAsync(Appointment appointment);

        Task CreateAppointmentDetailAsync(AppointmentDetail appointmentDetail);

        Task<AppointmentDetail> GetAppointmentDetailAsync(int appointmentId, int appointmentDetailId);
        Task UpdateAppointmentDetailAsync(AppointmentDetail appointmentDetail);






    }
}
