using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Services.IServices;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {

            var response=await _authService.RegisterAsync(registerRequestDTO);
            if (!response.Success)
                return BadRequest(response);
            return StatusCode(201, response);

        }

        [HttpPost]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO  loginRequestDTO)
        {

            var response=await _authService.LoginAsync(loginRequestDTO);
            if (response == null)
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            return Ok(response);

        }

        [HttpGet("getRoles")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            var roles = await _authService.GetRolesAsync();

            return Ok(roles);
        }
    }
}
