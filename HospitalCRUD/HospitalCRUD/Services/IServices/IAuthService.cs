using HospitalCRUD.Models.DTO.Auth;

namespace HospitalCRUD.Services.IServices
{
    public interface IAuthService
    {
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO registerRequestDto);
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDto);
        Task<IEnumerable<RoleDTO>> GetRolesAsync();
    }
}
