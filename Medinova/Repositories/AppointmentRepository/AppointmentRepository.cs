using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Repositories.AppointmentRepository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly MedinovaContext context;

        public AppointmentRepository()
        {
            context = new MedinovaContext();
        }
        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();
        }

        public async Task<Appointment> GetAppointmentAsync(int appointmentId)
        {
            return await context.Appointments.Include(x => x.AppointmentDetails).Where(x => x.AppointmentId == appointmentId).FirstOrDefaultAsync();
        }

        public async Task<List<Appointment>> GetListAppointmentAsync()
        {
            var twoMonthsAgo = DateTime.Today.AddMonths(-2);

            return await context.Appointments
                .Where(x => x.AppointmentDate >= twoMonthsAgo
                         && x.AppointmentDate <= DateTime.Today)
                .OrderByDescending(x => x.AppointmentDate)
                .ToListAsync();
        }

    }
}