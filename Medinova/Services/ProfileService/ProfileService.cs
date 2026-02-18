using Medinova.DTOs.ProfileDtos;
using Medinova.Models;
using Medinova.Repositories.ProfileRepository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.ProfileService
{
    public class ProfileService : IProfileService

    {
        MedinovaContext context = new MedinovaContext();
        private readonly IProfileRepository _profileRepository;
        public ProfileService()
        {
            _profileRepository = new ProfileRepository();
        }
        //public async Task<UpdatePatientDto> GetByIdUser(int userId)
        //{

        //    var value = await _profileRepository.GetByIdUser(userId);
        //    var today = DateTime.Now;
        //    var birthDate = value.BirthDate.Value;
        //    var age = today.Year - birthDate.Year;
        //    return new UpdateUserDto
        //    {
        //        UserId = userId,
        //        RoleName = value.Roles.Select(x => x.RoleName).FirstOrDefault(),
        //        IdentityNumber = value.IdentityNumber,
        //        PhoneNumber = value.PhoneNumber,
        //        Email = value.Email,
        //        FirstName= value.FirstName,
        //        LastName= value.LastName,
        //        ImageUrl= value.ImageUrl,
        //        IsActive= value.IsActive,
        //        CreatedDate= value.CreatedDate,
        //        BirthDate=(DateTime)value.BirthDate,
        //        Age = age,
        //        BloodType= value.BloodType,
        //        GenderId = value.GenderId,
        //        ConfirmPassword=value.Password

        //    };
        //}

      
    }
}