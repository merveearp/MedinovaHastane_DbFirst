using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.ProfileRepository
{
    public interface IProfileRepository
    {
        Task<User> GetByIdUser(int userId);
       
    }
}
