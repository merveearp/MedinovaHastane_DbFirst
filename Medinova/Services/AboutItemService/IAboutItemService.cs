using Medinova.DTOs.AboutItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.AboutItemService
{
    public interface IAboutItemService :IGenericService<ResultAboutItemDto,UpdateAboutItemDto,CreateAboutItemDto>
    {
    }
}
