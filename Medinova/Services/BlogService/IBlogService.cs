using Medinova.DTOs.BlogDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.BlogService
{
    public interface IBlogService :IGenericService<ResultBlogDto,UpdateBlogDto,CreateBlogDto>
    {
    }
}
