using HospitalCRUD.Data;
using HospitalCRUD.Models;
using HospitalCRUD.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalCRUD.Repositories
{
    public class AdmissionRepository: Repository<Admission>, IAdmissionRepository
    {
        public AdmissionRepository(AppDbContext context)
    : base(context)
        {
        }

        public async Task<List<Admission>> GetAllPaginationWithPatientDoctorAsync(
    int page,
    int pageSize
            , string? search, string? sortBy, string? sortDirection)
        {

            var result = _dbSet
                 .Include(x => x.AttendingDoctor)
                     .Include(x => x.Patient)
            .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim().ToLower();

                result = result.Where(x =>
                    x.AttendingDoctor.FirstName.ToLower().Contains(searchTerm) ||
                    x.AttendingDoctor.LastName.ToLower().Contains(searchTerm) ||
                    x.Patient.FirstName.ToLower().Contains(searchTerm) ||
                    x.Patient.LastName.ToLower().Contains(searchTerm) ||
                    x.Diagnosis.ToLower().Contains(searchTerm) ||
                    x.AdmissionDate.ToString().Contains(searchTerm) ||
                   (x.DischargeDate.HasValue &&
 x.DischargeDate.Value.ToString().Contains(searchTerm))
                    );
            }

            // Sort
            if (sortBy == "diagnosis")
            {
                result = sortDirection == "desc"
                    ? result.OrderByDescending(x => x.Diagnosis)
                    : result.OrderBy(x => x.Diagnosis);
            }
           
            else
            {
                
                result = result.OrderBy(x => x.Patient.FirstName).ThenBy(x=>x.Patient.LastName);
            }

            // Pagination
            return await result


                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        }


        public async Task<Admission?> GetByIdWithPatientDoctorAsync(int id)
        {
            return await _dbSet.Include(x => x.AttendingDoctor)
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.AdmissionId == id);
        }

        public async Task<int> GetCountAsync(string? search)
        {
            //  return await _dbSet.CountAsync();
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                //query = query.Where(x => x.FirstName.Contains(search));
                query = query.Where(x =>
                  x.AttendingDoctor.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.AttendingDoctor.LastName.ToLower().Contains(search.ToLower()) ||
                    x.Patient.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.Patient.LastName.ToLower().Contains(search.ToLower()) ||
                    x.Diagnosis.ToLower().Contains(search.ToLower()) ||
                    x.AdmissionDate.ToString().Contains(search.ToLower()) ||
                   (x.DischargeDate.HasValue &&
 x.DischargeDate.Value.ToString().Contains(search.ToLower()))
                );

            }

            return await query.CountAsync();
        }
    }
}
