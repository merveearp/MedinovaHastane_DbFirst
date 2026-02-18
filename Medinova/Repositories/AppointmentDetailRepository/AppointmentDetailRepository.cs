using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Repositories.AppointmentDetailRepository
{
    public class AppointmentDetailRepository : IAppointmentDetailRepository
    {
        private readonly MedinovaContext _context;
        public AppointmentDetailRepository()
        {
            _context = new MedinovaContext();
        }
        public async Task<List<AppointmentDetail>> GetAllAppointmentDetailAsync()
        {
            var twoMonthsAgo = DateTime.Today.AddMonths(-2);

            return await _context.AppointmentDetails
                .Include(x => x.Appointment)
                .Where(x => x.Appointment.AppointmentDate >= twoMonthsAgo
                         && x.Appointment.AppointmentDate <= DateTime.Today)
                .AsNoTracking()
                .OrderByDescending(x => x.Appointment.AppointmentDate)
                .ToListAsync();
        }


    }
}