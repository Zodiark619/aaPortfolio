using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Doctor;
using HospitalCRUD.Models.DTO.DoctorSpecialty;

namespace HospitalCRUD.Services.IServices
{
    public interface IDoctorService
    {
        Task<PagedResult<DoctorDTO>> GetAllAsync(
 PaginationRequest request);
        Task<DoctorDTO?> GetByIdAsync(
    int id);

        Task<DoctorDTO?> CreateAsync(
    CreateDoctorDTO dto);
        Task<DoctorDTO?> UpdateAsync(int id,
    UpdateDoctorDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DropdownDTO>> GetAllDropdownAsync();

    }
}
