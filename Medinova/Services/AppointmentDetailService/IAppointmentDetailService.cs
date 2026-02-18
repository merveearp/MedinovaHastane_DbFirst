using Medinova.DTOs.AppointmentDetailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.AppointmentDetailService
{
    public interface IAppointmentDetailService
    {
        Task<List<ResultAppointmentDetailDto>> GetAllAppointmentDetailAsync();
    }
}
