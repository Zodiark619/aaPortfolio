using HospitalCRUD.Data;
using HospitalCRUD.Models;
using HospitalCRUD.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalCRUD.Repositories
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        public ProvinceRepository(AppDbContext context)
     : base(context)
        {
        }
        public async Task<List<Province>> GetAllPaginationAsync(
      int page,
      int pageSize
              , string? search, string? sortBy, string? sortDirection)
        {

            var result = _dbSet
            .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim().ToLower();

                result = result.Where(x =>
                    x.ProvinceId.ToLower().Contains(searchTerm) ||
                    x.ProvinceName.ToLower().Contains(searchTerm)  );
            }

            // Sort
            if (sortBy == "provinceId")
            {
                result = sortDirection == "desc"
                    ? result.OrderByDescending(x => x.ProvinceId)
                    : result.OrderBy(x => x.ProvinceId);
            }
            else if (sortBy == "provinceName")
            {
                result = sortDirection == "desc"
                    ? result.OrderByDescending(x => x.ProvinceName)
                    : result.OrderBy(x => x.ProvinceName);
            }
            
            else
            {
                // Default sort
                result = result.OrderBy(x => x.ProvinceName);
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
                query = query.Where(x => x.ProvinceId.ToLower().Contains(search.ToLower()) ||
                                         x.ProvinceName.ToLower().Contains(search.ToLower()));
            }

            return await query.CountAsync();
        }
        public async Task<Province?> GetByStringIdAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ProvinceId == id);
        }
        //public async Task DeleteStringAsync(string id)
        //{
        //      await _dbSet.FirstOrDefaultAsync(x => x.ProvinceId == id);
        //}

    }
}
