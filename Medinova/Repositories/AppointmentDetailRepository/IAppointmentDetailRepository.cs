using Medinova.DTOs.AppointmentDetailDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.AppointmentDetailRepository
{
    public interface IAppointmentDetailRepository
    {
        Task<List<AppointmentDetail>> GetAllAppointmentDetailAsync();
        //Task<AppointmentDetail> GetAppointmentDetailAsync();
    }
}
