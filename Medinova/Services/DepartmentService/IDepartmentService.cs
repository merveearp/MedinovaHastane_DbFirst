using Medinova.DTOs.DepartmentDtos;
using Medinova.DTOs.DoctorDtos;
using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.DepartmentService
{
    public interface IDepartmentService 
    {
        Task<List<ResultDepartmentDto>> GetAllAsync();
        Task<List<ResultDoctorDto>> GetDoctorsByDepartmentIdAsync(int departmentId);
        Task<int> GetCountDoctorsByDepartmentIdAsync(int departmentId);
        Task<UpdateDepartmentDto> GetByIdAsync(int id);
        Task CreateAsync(CreateDepartmentDto dto);
        Task UpdateAsync(UpdateDepartmentDto dto);
        Task DeleteAsync(int id);
    }
}
