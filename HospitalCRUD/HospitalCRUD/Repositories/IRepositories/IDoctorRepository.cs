using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<Doctor>> GetPagedWithSpecialtyAsync(
       int page,
       int pageSize
   );
        Task<Doctor?> GetByIdWithSpecialtyAsync(  int id );

        Task<int> GetCountAsync(string? search);

    }
}
