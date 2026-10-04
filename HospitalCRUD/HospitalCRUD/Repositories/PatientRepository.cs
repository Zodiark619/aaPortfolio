using HospitalCRUD.Data;
using HospitalCRUD.Models;
using HospitalCRUD.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalCRUD.Repositories
{
    public class PatientRepository: Repository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context)
    : base(context)
        {
        }

        public async Task<List<Patient>> GetAllPaginationWithProvinceAsync(
    int page,
    int pageSize
            , string? search, string? sortBy, string? sortDirection)
        {

            var result = _dbSet
                 .Include(x => x.Province)
            .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim().ToLower();

                result = result.Where(x =>
                    x.FirstName.ToLower().Contains(searchTerm) ||
                    x.LastName.ToLower().Contains(searchTerm) ||
                    x.Gender.ToLower().Contains(searchTerm) ||
                    x.DateOfBirth.ToString().Contains(searchTerm) ||
                    x.City.ToLower().Contains(searchTerm) ||

                    x.Province.ProvinceId.ToLower().Contains(searchTerm) ||

                    x.Province.ProvinceName.ToLower().Contains(searchTerm)||
                    x.Allergies.ToLower().Contains(searchTerm) ||
                    x.Height.ToString().Contains(searchTerm) ||
                    x.Weight.ToString().Contains(searchTerm) 

                    );
            }

            // Sort
            if (sortBy == "firstName")
            {
                result = sortDirection == "desc"
                    ? result.OrderByDescending(x => x.FirstName)
                    : result.OrderBy(x => x.FirstName);
            }
            //else if (sortBy == "lastName")
            //{
            //    result = sortDirection == "desc"
            //        ? result.OrderByDescending(x => x.LastName)
            //        : result.OrderBy(x => x.LastName);
            //}
            //else if (sortBy == "PatientProvince")
            //{
            //    result = sortDirection == "desc"
            //        ? result.OrderByDescending(x => x.Province.Name)
            //        : result.OrderBy(x => x.Province.Name);
            //}
            else
            {
                // Default sort
                result = result.OrderBy(x => x.PatientId);
            }

            // Pagination
            return await result


                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        }


        public async Task<Patient?> GetByIdWithProvinceAsync(int id)
        {
            return await _dbSet.Include(x => x.Province).FirstOrDefaultAsync(x => x.PatientId == id);
        }

        public async Task<int> GetCountAsync(string? search)
        {
            //  return await _dbSet.CountAsync();
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                //query = query.Where(x => x.FirstName.Contains(search));
                query = query.Where(x =>
                 x.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.LastName.ToLower().Contains(search.ToLower()) ||
                    x.Gender.ToLower().Contains(search.ToLower()) ||
                    x.DateOfBirth.ToString().Contains(search.ToLower()) ||
                    x.City.ToLower().Contains(search.ToLower()) ||

                    x.Province.ProvinceId.ToLower().Contains(search.ToLower()) ||

                    x.Province.ProvinceName.ToLower().Contains(search.ToLower()) ||
                    x.Allergies.ToLower().Contains(search.ToLower()) ||
                    x.Height.ToString().Contains(search.ToLower()) ||
                    x.Weight.ToString().Contains(search.ToLower())
                );

            }

            return await query.CountAsync();
        }
    }
}
