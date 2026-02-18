using Medinova.Models;
using Medinova.Repositories.GenericSingleRepository;

namespace Medinova.Repositories.ContactRepository
{
    public class ContactRepository : GenericSingleRepository<Contact>, IContactRepository
    {
        public ContactRepository(MedinovaContext context) : base(context)
        {
        }
    }
}