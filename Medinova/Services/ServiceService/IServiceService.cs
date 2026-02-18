using Medinova.DTOs.ServicesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.ServiceService
{
    public interface IServiceService : IGenericService<ResultServiceDto , UpdateServiceDto , CreateServiceDto>
    {

    }
}
