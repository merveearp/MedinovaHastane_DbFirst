using Medinova.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Repositories.GenericSingleRepository
{
    public class GenericSingleRepository<T> : IGenericSingleRepository<T> where T : class
    {
        private readonly MedinovaContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericSingleRepository(MedinovaContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetAsync()
        {
            return  await _dbSet.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

      
    }
}