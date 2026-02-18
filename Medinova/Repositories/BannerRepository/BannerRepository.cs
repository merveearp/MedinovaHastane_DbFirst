using Medinova.Models;
using Medinova.Repositories.GenericSingleRepository;

namespace Medinova.Repositories.BannerRepository
{
    public class BannerRepository : GenericSingleRepository<Banner>, IBannerRepository
    {
        public BannerRepository(MedinovaContext context) : base(context)
        {
        }
    }
}