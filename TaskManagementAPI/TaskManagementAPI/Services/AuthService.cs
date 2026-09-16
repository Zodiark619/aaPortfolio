using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Services.IServices;

namespace TaskManagementAPI.Services
{
    public class AuthService : IAuthService
    { 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService( UserManager<IdentityUser> userManager,IConfiguration configuration)
        { 
            _userManager = userManager;
            _configuration = configuration;
        }
        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
        new Claim(
            ClaimTypes.NameIdentifier,
            user.Id
        ),
        new Claim(
            ClaimTypes.Email,
            user.Email!
        )
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request  )
        { 
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return null;
             
            var passwordValid = await _userManager.CheckPasswordAsync(
                user,
                request.Password
            );

            if (!passwordValid)
                return null;
             
            var token = GenerateJwtToken(user);
             
            return new LoginResponseDTO
            {
                Token = token,
                Message="Login successful"
            };
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO registerRequestDto)
        {
            if (registerRequestDto.Role != "Admin" &&
    registerRequestDto.Role != "Member")
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Message = "Invalid role."
                };
            }
            var user = new IdentityUser
            {
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if (!result.Succeeded)
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Message = "Registration failed."
                };
            }
            var roleResult = await _userManager.AddToRoleAsync(
        user,
        registerRequestDto.Role
    );

            if (!roleResult.Succeeded)
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Message = "User created, but role assignment failed."
                };
            }

            return new RegisterResponseDTO
            {
                Success = true,
                Message = "Registration successful."
            };
        }

         
    }
}
