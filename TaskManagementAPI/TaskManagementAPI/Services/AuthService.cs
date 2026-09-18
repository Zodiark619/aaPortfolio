using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService( UserManager<IdentityUser> userManager,IConfiguration configuration,RoleManager<IdentityRole> roleManager)
        { 
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
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
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
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
             
            var token = await GenerateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            return new LoginResponseDTO
            {
                Token = token,
                Message="Login successful",
                Email=user.Email,
                Role=role
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


        public async Task<IEnumerable<RoleDTO>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles
               .Select(role => new RoleDTO
               {

                   Name = role.Name!
               })
               .ToListAsync();

            return  roles;
        }











    }
}
