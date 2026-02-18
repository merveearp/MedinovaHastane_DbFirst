using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.AppointmentRepository
{
    public interface IAppointmentRepository
    {
        Task CreateAppointmentAsync(Appointment appointment);
        Task<List<Appointment>> GetListAppointmentAsync();
        Task<Appointment> GetAppointmentAsync(int appointmentId);

    }
}
