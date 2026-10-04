using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IPatientRepository: IRepository<Patient>
    {
        Task<List<Patient>> GetAllPaginationWithProvinceAsync(
            int page,
            int pageSize,
            string? search,
            string? sortBy,
            string? sortDirection);
        Task<Patient?> GetByIdWithProvinceAsync(int id);

        Task<int> GetCountAsync(string? search);
    }
}
