using Medinova.DTOs.AboutItemDtos;
using Medinova.Models;
using Medinova.Repositories.AboutItemRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.AboutItemService
{
    public class AboutItemService : IAboutItemService
    {
        private readonly IAboutItemRepository _aboutItemRepository;
        public AboutItemService()
        {
            _aboutItemRepository = new AboutItemRepository(new MedinovaContext());
        }
        public async Task CreateAsync(CreateAboutItemDto dto)
        {
            var aboutitem = new AboutItem
            {
                Title = dto.Title,
                Icon = dto.Icon,
                Name = dto.Name
            };
            await _aboutItemRepository.CreateAsync(aboutitem);

        }

        public async Task DeleteAsync(int id)
        {
            await _aboutItemRepository.DeleteAsync(id);
        }

        public async Task<List<ResultAboutItemDto>> GetAllAsync()
        {
            var values = await _aboutItemRepository.GetAllAsync();

            return values.Select(x => new ResultAboutItemDto
            {
                AboutItemId = x.AboutItemId,
                Title = x.Title,
                Name = x.Name,
                Icon = x.Icon
            }).ToList();
        }


        public async Task<UpdateAboutItemDto> GetByIdAsync(int id)
        {
            var value = await _aboutItemRepository.GetByIdAsync(id);

            return new UpdateAboutItemDto
            {
                AboutItemId = value.AboutItemId,
                Title = value.Title,
                Icon = value.Icon,
                Name = value.Name
            };

        }

        public async Task UpdateAsync(UpdateAboutItemDto dto)
        {
            var aboutItem = await _aboutItemRepository.GetByIdAsync(dto.AboutItemId);

            aboutItem.Title = dto.Title;
            aboutItem.Icon = dto.Icon;
            aboutItem.Name = dto.Name;

            await _aboutItemRepository.UpdateAsync(aboutItem);
        }

    }
}