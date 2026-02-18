using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.PatientRepository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task<List<Patient>> GetAllByDoctorIdAsync(int doctorId);
        Task<Patient> GetDetailByIdAsync(int id);
        Task<List<Appointment>> GetListAppointmentByPatientIdAsync(int patientId);
        Task<List<Appointment>> GetListAppointmentByPatientIdDoctorIdAsync(int patientId, int doctorId);


        Task<Appointment> GetAppointmentByPatientIdAsync(int patientId, int appointmentId);
        Task<AppointmentDetail> GetAppointmentDetailByPatientAsync(int appointmentId, bool isCompleted);

        Task UpdateAppointmentByPatientIdAsync(Appointment appointment);
    }
}
