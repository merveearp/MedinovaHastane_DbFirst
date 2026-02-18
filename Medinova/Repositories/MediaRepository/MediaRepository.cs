using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.Repositories.MediaRepository
{
    public class MediaRepository : GenericRepository<Media>, IMediaRepository
    {
        public MediaRepository(MedinovaContext context) : base(context)
        {
        }
    }
}