using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.DoctorSpecialty;
using HospitalCRUD.Models.DTO.Province;

namespace HospitalCRUD.Services.IServices
{
    public interface IProvinceService
    {
        Task<PagedResult<ProvinceDTO>> GetAllAsync(
PaginationRequest request);
        Task<ProvinceDTO?> GetByIdAsync(
    string id);

        Task<ProvinceDTO> CreateAsync(
    CreateProvinceDTO dto);
        Task<ProvinceDTO?> UpdateAsync(string id,
    UpdateProvinceDTO dto);
        Task<bool> DeleteAsync(string id);


        Task<IEnumerable<ProvinceDropdownDTO>> GetAllDropdownAsync();
    }
}
