using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IDoctorSpecialtyRepository: IRepository<DoctorSpecialty>
    {
        Task<List<DoctorSpecialty>> GetAllPaginationAsync(
       int page,
       int pageSize,
       string? search,
       string? sortBy,
       string? sortDirection);

        Task<int> GetCountAsync(string? search);



    }
}
