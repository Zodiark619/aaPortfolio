using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.DTO;

namespace TaskManagementAPI.Services.IServices
{
    public interface ITaskService
    {

        Task<TodoTask> CreateTaskAsync(CreateTodoTaskDTO createTaskDTO, string userId);






        Task<TodoTaskPagedResponse> GetAllAsync( 
               int page,
               int pageSize
            );
        
        Task<TodoTaskResponseDTO?> GetAsync(int toDoId 
            );
       



    }
}
