using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        //     Task<List<Doctor>> GetPagedWithSpecialtyAsync(
        //    int page,
        //    int pageSize
        //);
        Task<List<Doctor>> GetAllPaginationWithSpecialtyAsync(
            int page,
            int pageSize,
            string? search,
            string? sortBy,
            string? sortDirection);
        Task<Doctor?> GetByIdWithSpecialtyAsync(  int id );

        Task<int> GetCountAsync(string? search);

    }
}
