using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IProvinceRepository : IRepository<Province>
    {
        Task<List<Province>> GetAllPaginationAsync(
      int page,
      int pageSize,
      string? search,
      string? sortBy,
      string? sortDirection);

        Task<int> GetCountAsync(string? search);
        Task<Province?> GetByStringIdAsync(string id);
      //  Task DeleteStringAsync(string id);

    }
}
