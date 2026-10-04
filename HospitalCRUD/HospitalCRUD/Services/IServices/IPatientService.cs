using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Patient;

namespace HospitalCRUD.Services.IServices
{
    public interface IPatientService
    {
        Task<PagedResult<PatientDTO>> GetAllAsync(
PaginationRequest request);
        Task<PatientDTO?> GetByIdAsync(
    int id);

        Task<PatientDTO?> CreateAsync(
    CreatePatientDTO dto);
        Task<PatientDTO?> UpdateAsync(int id,
    UpdatePatientDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DropdownDTO>> GetAllDropdownAsync();

    }
}
