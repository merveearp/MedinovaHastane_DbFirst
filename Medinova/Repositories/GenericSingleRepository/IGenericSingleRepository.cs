using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.GenericSingleRepository
{
    public interface IGenericSingleRepository<T> where T : class
    {
        Task<T> GetAsync();
        Task UpdateAsync(T entity); 
        
    }
}
