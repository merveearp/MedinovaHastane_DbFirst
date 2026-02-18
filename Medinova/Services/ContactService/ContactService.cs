using Medinova.DTOs.ContactDtos;
using Medinova.Models;
using Medinova.Repositories.ContactRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService()
        {
            _contactRepository= new ContactRepository(new MedinovaContext());
        }
        public async Task<ResultContactDto> GetAsync()
        {
            var value = await _contactRepository.GetAsync();
            return new ResultContactDto
            {
                ContactId = value.ContactId,
                Address = value.Address,
                Mail = value.Mail,
                PhoneNumber = value.PhoneNumber,
                WorkingHours= value.WorkingHours
            };
        }

        public async Task UpdateAsync(ResultContactDto dto)
        {
            var value = await _contactRepository.GetAsync();

            value.Address = dto.Address;
            value.Mail = dto.Mail;
            value.PhoneNumber = dto.PhoneNumber;
            value.WorkingHours = dto.WorkingHours;
           

            await _contactRepository.UpdateAsync(value);

        }
    }
}