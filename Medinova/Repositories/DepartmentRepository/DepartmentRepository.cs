using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MedinovaContext _context;

        public DepartmentRepository()
        {
            _context = new MedinovaContext();
        }

        public async Task CreateAsync(Department entity)
        {
            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           
            var hasActiveDoctor = await _context.Doctors
                .Include(x => x.User)
                .AnyAsync(x => x.DepartmentId == id && x.User.IsActive);

            if (hasActiveDoctor)
                throw new InvalidOperationException(
                    "Bu departmana bağlı aktif doktorlar bulunduğu için silinemez."
                );

            var department = await _context.Departments
                .FirstOrDefaultAsync(x => x.DepartmentId == id);

            if (department == null)
                return;

            department.IsActive = false;

            await _context.SaveChangesAsync();
        }


        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }


        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<int> GetCountDoctorsByDepartmentIdAsync(int departmentId)
        {
            return await _context.Doctors.CountAsync(x => x.DepartmentId == departmentId);
        }


        public async Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int departmentId)
        {
            return await _context.Doctors.Include(x => x.User).Include(x => x.Department).Where(x => x.DepartmentId == departmentId).ToListAsync();
        }

        public async Task UpdateAsync(Department entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

      

    }
}