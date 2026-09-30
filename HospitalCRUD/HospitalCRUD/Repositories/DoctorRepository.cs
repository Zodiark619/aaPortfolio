using HospitalCRUD.Data;
using HospitalCRUD.Models;
using HospitalCRUD.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalCRUD.Repositories
{
    public class DoctorRepository: Repository<Doctor>, IDoctorRepository
    {

         

        public DoctorRepository(AppDbContext context)
       : base(context)
        {
        }

        public async Task<List<Doctor>> GetPagedWithSpecialtyAsync(
    int page,
    int pageSize)
        {
            return await _dbSet
                .Include(x => x.Specialty)
                 .OrderBy(x => x.DoctorId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<Doctor?> GetByIdWithSpecialtyAsync(int id)
        {
            return await _dbSet.Include(x => x.Specialty).FirstOrDefaultAsync(x=>x.DoctorId==id);
        }

        public async Task<int> GetCountAsync(string? search)
        {
            //  return await _dbSet.CountAsync();
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.FirstName.Contains(search));
            }

            return await query.CountAsync();
        }
    }
}
