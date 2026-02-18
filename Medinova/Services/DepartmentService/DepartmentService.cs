using Medinova.DTOs.DepartmentDtos;
using Medinova.DTOs.DoctorDtos;
using Medinova.Models;
using Medinova.Repositories.DepartmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.DepartmentService
{
    public class DepartmentService :IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService()
        {
            _departmentRepository = new DepartmentRepository();
            
        }

        public async Task CreateAsync(CreateDepartmentDto dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                IsActive = true
            };

            await _departmentRepository.CreateAsync(department);
        }

        public async Task DeleteAsync(int id)
        {
           await _departmentRepository.DeleteAsync(id);
        }

        public async Task<List<ResultDepartmentDto>> GetAllAsync()
        {
            var values = await _departmentRepository.GetAllAsync();
            return values.Select(x => new ResultDepartmentDto
            {
                DepartmentId= x.DepartmentId,
                Name= x.Name,
                IsActive= x.IsActive

            }).ToList();
        }

        public async Task<UpdateDepartmentDto> GetByIdAsync(int id)
        {
           var value = await _departmentRepository.GetByIdAsync(id);

            return new UpdateDepartmentDto
            {
                DepartmentId = value.DepartmentId,
                Name = value.Name
                
            };
        }

        public async Task<int> GetCountDoctorsByDepartmentIdAsync(int departmentId)
        {
           return await _departmentRepository.GetCountDoctorsByDepartmentIdAsync(departmentId);
        }

        public async Task<List<ResultDoctorDto>> GetDoctorsByDepartmentIdAsync(int departmentId)
        {
            var values = await _departmentRepository.GetDoctorsByDepartmentIdAsync(departmentId);

            return values.Select(x => new ResultDoctorDto
            {

                DoctorId = x.DoctorId,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.Name,
                Title=x.Title,
                FirstName=x.User.FirstName,
                LastName=x.User.LastName,
                ImageUrl=x.User.ImageUrl



            }).ToList();
        }

        public async Task UpdateAsync(UpdateDepartmentDto dto)
        {
            var value = await _departmentRepository.GetByIdAsync(dto.DepartmentId);
            value.Name = dto.Name;
            await _departmentRepository.UpdateAsync(value);


        }
    }
}