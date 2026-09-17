using Microsoft.AspNetCore.Identity.Data;
using TaskManagementAPI.Models.DTO;

namespace TaskManagementAPI.Services.IServices
{
    public interface IAuthService
    {


        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO registerRequestDto);
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDto);
        Task<IEnumerable<RoleDTO>> GetRolesAsync();
    }
}
