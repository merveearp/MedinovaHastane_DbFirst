using Medinova.DTOs.AboutDtos;
using Medinova.Models;
using Medinova.Repositories.AboutRepository;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Medinova.Services.AboutService
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;

        public AboutService()
        {
            _aboutRepository = new AboutRepository(new MedinovaContext());
        }
        public async Task<ResultAboutDto> GetAsync()
        {
           var value = await _aboutRepository.GetAsync();
            return new ResultAboutDto
            {
                AboutId = value.AboutId,
                Description = value.Description,
                ImageUrl = value.ImageUrl,
                Title = value.Title
            };
        }

        public async Task UpdateAsync(ResultAboutDto dto)
        {
            var about = await _aboutRepository.GetAsync();

            about.Title = dto.Title;
            about.Description = dto.Description;
            about.ImageUrl = dto.ImageUrl;

            await _aboutRepository.UpdateAsync(about);
        }

       
    }
}