using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Repositories.PatientRepository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MedinovaContext _context;
        public PatientRepository()
        {
            _context = new MedinovaContext();
        }
        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.AsNoTracking().ToListAsync();
        }

        public async Task<List<Patient>> GetAllByDoctorIdAsync(int doctorId)
        {
            return await _context.Patients.Where(p => p.Appointments.Any(a => a.DoctorId == doctorId)).AsNoTracking().ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByPatientIdAsync(int patientId, int appointmentId)
        {
            return await _context.Appointments.Where(x => x.PatientId == patientId).Where(x => x.AppointmentId == appointmentId).FirstOrDefaultAsync();
        }

        public async Task<AppointmentDetail> GetAppointmentDetailByPatientAsync(int appointmentId, bool isCompleted)
        {
            return await _context.AppointmentDetails.Where(x => x.AppointmentId == appointmentId).Where(x => x.Appointment.IsCompleted == true).FirstOrDefaultAsync();
        }

        public async Task<Patient> GetDetailByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(x=>x.PatientId==id);
        }

        public async Task<List<Appointment>> GetListAppointmentByPatientIdAsync(int patientId)
        {
            return await _context.Appointments.Where(x=>x.PatientId==patientId).OrderByDescending(x=>x.AppointmentDate).ToListAsync();
        }

        public async Task<List<Appointment>> GetListAppointmentByPatientIdDoctorIdAsync(int patientId, int doctorId)
        {
            return await _context.Appointments.Where(x => x.PatientId == patientId && x.DoctorId==doctorId).OrderByDescending(x => x.AppointmentDate).ToListAsync();
        }

        public async Task UpdateAppointmentByPatientIdAsync(Appointment appointment)
        {
           if(appointment ==null)
            {
                return;
            }

            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}