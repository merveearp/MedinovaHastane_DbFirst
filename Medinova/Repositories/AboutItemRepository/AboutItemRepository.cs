using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.Repositories.AboutItemRepository
{
    public class AboutItemRepository : GenericRepository<AboutItem>, IAboutItemRepository
    {
        public AboutItemRepository(MedinovaContext context) : base(context)
        {
        }
    }
}