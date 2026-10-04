using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IAdmissionRepository: IRepository<Admission>
    {
        Task<List<Admission>> GetAllPaginationWithPatientDoctorAsync(
       int page,
       int pageSize,
       string? search,
       string? sortBy,
       string? sortDirection);

        Task<int> GetCountAsync(string? search);
        Task<Admission?> GetByIdWithPatientDoctorAsync(int id);

    }
}
