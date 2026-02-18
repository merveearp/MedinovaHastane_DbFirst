using Medinova.DTOs.BannerDtos;
using Medinova.Models;
using Medinova.Repositories.BannerRepository;
using System;
using System.Threading.Tasks;

namespace Medinova.Services.BannerService
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        public BannerService()
        {
            _bannerRepository = new BannerRepository(new MedinovaContext());
        }

        public async Task<ResultBannerDto> GetAsync()
        {
            var value = await _bannerRepository.GetAsync();
            return new ResultBannerDto 
            {
                BannerId = value.BannerId,
                Title = value.Title,
                Description = value.Description,
                ImageUrl = value.ImageUrl

            };

        }
            

        public async Task UpdateAsync(ResultBannerDto dto)
        {
            var banner = await _bannerRepository.GetAsync();
   
            banner.Title = dto.Title;
            banner.Description = dto.Description;
            banner.ImageUrl = dto.ImageUrl;

            await _bannerRepository.UpdateAsync(banner);
        }
    }
}