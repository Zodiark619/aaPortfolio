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

            public async Task<List<Doctor>> GetAllPaginationWithSpecialtyAsync(
        int page,
        int pageSize
                , string? search, string? sortBy, string? sortDirection)
            {
            
                var result = _dbSet
                     .Include(x => x.Specialty)
                .AsQueryable();

                // Search
                if (!string.IsNullOrWhiteSpace(search))
                {
                var searchTerm = search.Trim().ToLower();

                result = result.Where(x =>
                    x.FirstName.ToLower().Contains(searchTerm) ||
                    x.LastName.ToLower().Contains(searchTerm) ||
                    x.Specialty.Name.ToLower().Contains(searchTerm));
            }

            // Sort
            if (sortBy == "firstName")
            {
                result = sortDirection == "desc"
                    ? result.OrderByDescending(x => x.FirstName)
                    : result.OrderBy(x => x.FirstName);
            }
            else if (sortBy == "lastName")
            {
                result = sortDirection == "desc"
                    ? result.OrderByDescending(x => x.LastName)
                    : result.OrderBy(x => x.LastName);
            }
            else if (sortBy == "doctorSpecialty")
            {
                result = sortDirection == "desc"
                    ? result.OrderByDescending(x => x.Specialty.Name)
                    : result.OrderBy(x => x.Specialty.Name);
            }
            else
            {
                // Default sort
                result = result.OrderBy(x => x.DoctorId);
            }

            // Pagination
            return await result
                             
                            
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
                //query = query.Where(x => x.FirstName.Contains(search));
                query = query.Where(x => 
                x.FirstName.ToLower().Contains(search.ToLower())||
                  x.LastName.ToLower().Contains(search.ToLower()) ||
                  x.Specialty.Name.ToLower().Contains(search.ToLower()) 
                );

            }

            return await query.CountAsync();
        }
    }
}
