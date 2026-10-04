using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Admission; 

namespace HospitalCRUD.Services.IServices
{
    public interface IAdmissionService
    {
        Task<PagedResult<AdmissionDTO>> GetAllAsync(
PaginationRequest request);
        Task<AdmissionDTO?> GetByIdAsync(
    int id);

        Task<AdmissionDTO?> CreateAsync(
    CreateAdmissionDTO dto);
        Task<AdmissionDTO?> UpdateAsync(int id,
    UpdateAdmissionDTO dto);
        Task<bool> DeleteAsync(int id); 
    }
}
