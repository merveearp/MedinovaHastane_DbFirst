using Medinova.DTOs.MediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.MediaService
{
    public interface IMediaService :IGenericService<ResultMediaDto , UpdateMediaDto, CreateMediaDto>
    {
    }
}
