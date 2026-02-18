using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int departmentId);
        Task<int> GetCountDoctorsByDepartmentIdAsync(int departmentId);
        Task<Department> GetByIdAsync(int id);
        Task CreateAsync(Department entity);
        Task UpdateAsync(Department entity);
        Task DeleteAsync(int id);

    }
}
