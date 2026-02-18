using Medinova.DTOs.MediaDtos;
using Medinova.Models;
using Medinova.Repositories.MediaRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.MediaService
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        public MediaService()
        {
            _mediaRepository = new MediaRepository(new MedinovaContext());
        }
        public async Task CreateAsync(CreateMediaDto dto)
        {
            var media = new Media
            {
                MediaName = dto.MediaName,
                MediaIcon = dto.MediaIcon,
                MediaUrl = dto.MediaUrl
            };
            await _mediaRepository.CreateAsync(media);
        }

        public async Task DeleteAsync(int id)
        {
            await _mediaRepository.DeleteAsync(id);
        }

        public async Task<List<ResultMediaDto>> GetAllAsync()
        {
            var values = await _mediaRepository.GetAllAsync();
            return values.Select(x => new ResultMediaDto
            {
               MediaId = x.MediaId, 
                MediaIcon = x.MediaIcon,
                MediaUrl = x.MediaUrl,
                MediaName = x.MediaName

            }).ToList();

        }

        public async Task<UpdateMediaDto> GetByIdAsync(int id)
        {
            var value = await _mediaRepository.GetByIdAsync(id);

            return new UpdateMediaDto
            {
                MediaId = value.MediaId,
                MediaIcon = value.MediaIcon,
                MediaUrl = value.MediaUrl,
                MediaName = value.MediaName
            };
               
        }

        public async Task UpdateAsync(UpdateMediaDto dto)
        {
            var value = await _mediaRepository.GetByIdAsync(dto.MediaId);

            value.MediaName = dto.MediaName;
            value.MediaIcon = dto.MediaIcon;
            value.MediaUrl = dto.MediaUrl;

            await _mediaRepository.UpdateAsync(value);
                
        }
    }
}