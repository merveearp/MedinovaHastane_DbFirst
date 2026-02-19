using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Repositories.DoctorRepository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MedinovaContext _context;
        public DoctorRepository()
        {
            _context = new MedinovaContext();
        }
        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.AsNoTracking().ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByDoctorIdAsync(int doctorId, int appointmentId)
        {
            return await _context.Appointments
                .Include(x => x.AppointmentDetails) 
                .Where(x =>
                    x.AppointmentId == appointmentId &&
                    x.DoctorId == doctorId)
                .FirstOrDefaultAsync();
        }


        public async Task<Doctor> GetDetailByIdAsync(int id)
        {
            return await _context.Doctors.Include(x=>x.User).Include(x=>x.Department).Include(x=>x.User.Gender).FirstOrDefaultAsync(x=> x.DoctorId == id);
        }


        public async Task<List<Appointment>> GetListAppointmentByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments.Where(x => x.DoctorId == doctorId).OrderByDescending(x => x.AppointmentDate).ToListAsync();
        }
        public async Task<AppointmentDetail> GetAppointmentDetailByDoctorAsync( int appointmentId, bool isCompleted)
        {
            return await _context.AppointmentDetails
                .Include(x => x.Appointment)
                .Where(x =>
                    x.AppointmentId == appointmentId &&
                    x.Appointment.IsCompleted == true)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAppointmentByDoctorIdAsync(Appointment appointment)
        {
            if (appointment == null)
                return;

            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreateAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            _context.AppointmentDetails.Add(appointmentDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            if (appointmentDetail == null)
                return;

            _context.Entry(appointmentDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<AppointmentDetail> GetAppointmentDetailAsync(int appointmentId, int appointmentDetailId)
        {
            return await _context.AppointmentDetails
               .Include(x => x.Appointment)
               .Where(x =>
                   x.AppointmentId == appointmentId &&
                   x.AppointmentDetailId== appointmentDetailId &&
                   x.Appointment.IsCompleted == true)
               .FirstOrDefaultAsync();
        }




    }
}