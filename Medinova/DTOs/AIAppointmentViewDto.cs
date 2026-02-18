using Medinova.DTOs.AIDtos;
using Medinova.DTOs.AppointmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.DTOs
{
    public class AIAppointmentViewDto
    {
        public List<ResultAppointmentDto> Appointments { get; set; }
        public AIResponseDto AIResponse { get; set; }
    }
}