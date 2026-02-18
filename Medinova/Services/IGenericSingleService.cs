using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services
{
    public interface IGenericSingleService<TResult> 
    {
        Task<TResult> GetAsync();
        Task UpdateAsync(TResult dto);
    }
}
