using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.DoctorSpecialty;

namespace HospitalCRUD.Services.IServices
{
    public interface IDoctorSpecialtyService
    {

        Task<PagedResult<DoctorSpecialtyDTO>> GetAllAsync(
    PaginationRequest request);
        Task<DoctorSpecialtyDTO?> GetByIdAsync(
    int id);

        Task<DoctorSpecialtyDTO> CreateAsync(
    CreateDoctorSpecialtyDTO dto);
        Task<DoctorSpecialtyDTO?> UpdateAsync(int id,
    UpdateDoctorSpecialtyDTO dto);
        Task<bool> DeleteAsync(int id);


        Task<IEnumerable<DropdownDTO>> GetAllDropdownAsync();
    }
}
