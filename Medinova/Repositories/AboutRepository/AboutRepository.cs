using Medinova.Models;
using Medinova.Repositories.GenericSingleRepository;


namespace Medinova.Repositories.AboutRepository
{
    public class AboutRepository : GenericSingleRepository<About>, IAboutRepository
    {

        public AboutRepository(MedinovaContext context) : base(context)
        {
        }
    }
}