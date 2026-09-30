using HospitalCRUD.Data;
using HospitalCRUD.Models;
using HospitalCRUD.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalCRUD.Repositories
{
    public class DoctorSpecialtyRepository : Repository<DoctorSpecialty>, IDoctorSpecialtyRepository
    {
        public DoctorSpecialtyRepository(AppDbContext context)
     : base(context)
        {
        }
        public  async Task<List<DoctorSpecialty>> GetAllPaginationAsync(int page, int pageSize, string? search, string? sortBy, string? sortDirection)
        {
            var result = _dbSet.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                result = result.Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()));
            }

            // Sort
            if (sortBy == "name")
            {
                if (sortDirection == "asc")
                {
                    result = result.OrderBy(x => x.Name);
                }
                else if (sortDirection == "desc")
                {
                    result = result.OrderByDescending(x => x.Name);
                }
            }

            // Pagination
            return await result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


            //  return await _dbSet.CountAsync();
        public async Task<int> GetCountAsync(string? search)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
            }

            return await query.CountAsync();
        }
    }
}
