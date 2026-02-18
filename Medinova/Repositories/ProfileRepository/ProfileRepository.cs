using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Medinova.Repositories.ProfileRepository
{
    public class ProfileRepository : IProfileRepository
    {
        MedinovaContext context = new MedinovaContext();
        public async Task<User> GetByIdUser(int userId)
        {
            return await context.Users.Include(x=>x.Roles).Where(x=>x.UserId==userId).FirstOrDefaultAsync();
        }

    }
}