using Medinova.DTOs.ServicesDtos;
using Medinova.Models;
using Medinova.Repositories.ServiceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.ServiceService
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceService()
        {
            _serviceRepository = new ServiceRepository(new MedinovaContext());
        }

        public async Task CreateAsync(CreateServiceDto dto)
        {
            var service = new Service
            {
                ServiceName = dto.ServiceName,
                ServiceIcon = dto.ServiceIcon,
                Description = dto.Description
            };

            await _serviceRepository.CreateAsync(service);
        }

        public async Task DeleteAsync(int id)
        {
           await _serviceRepository.DeleteAsync(id);
        }

        public async Task<List<ResultServiceDto>> GetAllAsync()
        {
           var values = await _serviceRepository.GetAllAsync();
            return values.Select(x => new ResultServiceDto
            {
                ServiceId = x.ServiceId,
                ServiceName = x.ServiceName,
                ServiceIcon = x.ServiceIcon,
                Description = x.Description

            }).ToList();
        }

        public async Task<UpdateServiceDto> GetByIdAsync(int id)
        {
            var value = await _serviceRepository.GetByIdAsync(id);

            return new UpdateServiceDto
            {
                ServiceIcon = value.ServiceIcon,
                Description = value.Description,
                ServiceName = value.ServiceName

            };
        }

        public async Task UpdateAsync(UpdateServiceDto dto)
        {
            var value = await _serviceRepository.GetByIdAsync(dto.ServiceId);

            value.ServiceIcon = dto.ServiceIcon;
            value.ServiceName = dto.ServiceName;
            value.Description = dto.Description;         

            await _serviceRepository.UpdateAsync(value);
        }
    }
}