using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Services.IServices;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TodoTaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        //[HttpGet("meong")]
        //[Authorize]
        //public IActionResult meong()
        //{
        //    return Ok();
        //}
        //[Authorize(Roles ="Admin")]
        //[HttpPost("create")]
        //public async Task<IActionResult> Create(CreateTodoTaskDTO createTodoTaskDTO)
        //{
        //    return Ok("CREATE ENDPOINT REACHED");
        //}
        [Authorize]
        [HttpPost("create")]

        public async Task<IActionResult> Create(CreateTodoTaskDTO createTodoTaskDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _taskService.CreateTaskAsync(
                createTodoTaskDTO, userId
            ); 
            return CreatedAtAction(
      nameof(GetById), 
         new { toDoId = result.Id },
      result
  );
        }
        [Authorize]
        [HttpGet("{toDoId}")] 
        public async Task<IActionResult> GetById(int toDoId
    )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _taskService.GetAsync(toDoId,
                userId
                
            );
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
    int page = 1,
    int pageSize = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _taskService.GetAllAsync(
                userId,
                page,
                pageSize
            );

            return Ok(result);
        }
    }
}
