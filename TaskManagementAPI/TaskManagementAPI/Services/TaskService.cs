using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Services.IServices;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TaskService(AppDbContext appDbContext,UserManager<IdentityUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<TodoTask> CreateTaskAsync(CreateTodoTaskDTO createTaskDTO,string userId)
        {
            var task = new TodoTask
            {
                Name=createTaskDTO.Name,
                Category=createTaskDTO.Category,
                Description=createTaskDTO.Description,
                CreatedAt=DateTime.UtcNow,
                CreatedBy = userId,
                IsCompleted=false,
                DueDate=createTaskDTO.DueDate ?? DateTime.UtcNow.AddDays(7),
                TaskStatus= Models.TaskStatus.Available
            };
            _appDbContext.TodoTasks.Add(task);
            await _appDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<TodoTaskPagedResponse> GetAllAsync(
    
    int page,
    int pageSize)
        {
            var query = _appDbContext.TodoTasks
               
                .OrderByDescending(t => t.CreatedAt);

            var totalItems = await query.CountAsync();
             
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TodoTaskResponseDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Category = t.Category,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt,
                    CreatedBy=t.CreatedBy,
                    SubmittedBy=t.SubmittedBy,
                    IsCompleted = t.IsCompleted,
                    DueDate = t.DueDate,
                    FinishedDate = t.FinishedDate,
                    TaskStatus= t.TaskStatus
                })
                .ToListAsync();
            //
            // Get all user IDs from the current page
            var userIds = items
                .Select(t => t.CreatedBy)
                .Distinct()
                .ToList();

            // Get users in one database query
            var users = await _userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(
                    u => u.Id,
                    u => u.UserName
                );

            // Add username to each task
            foreach (var item in items)
            {
                if (users.TryGetValue(item.CreatedBy, out var username))
                {
                    item.CreatedByName = username ?? "Unknown";
                }
                else
                {
                    item.CreatedByName = "Unknown";
                }
            }
            //
            var totalPages = (int)Math.Ceiling(
                totalItems / (double)pageSize
            );

            return new TodoTaskPagedResponse
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Items = items
            };
        }

        public async Task<TodoTaskResponseDTO?> GetAsync(int toDoId )
        {

            var task = await _appDbContext.TodoTasks
       .Where(x => x.Id == toDoId)
       .Select(t => new TodoTaskResponseDTO
       {
           Id = t.Id,
           Name = t.Name,
           Category = t.Category,
           Description = t.Description,
           CreatedAt = t.CreatedAt,
           CreatedBy = t.CreatedBy,
           SubmittedBy = t.SubmittedBy,
           IsCompleted = t.IsCompleted,
           DueDate = t.DueDate,
           FinishedDate = t.FinishedDate,
           TaskStatus = t.TaskStatus
       })
       .FirstOrDefaultAsync();

            if (task == null)
                return null;

            var user = await _userManager.Users
                .Where(u => u.Id == task.CreatedBy)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

            task.CreatedByName = user ?? "Unknown";

            return task;
            //    return await _appDbContext.TodoTasks
            // .Where(x=>x.Id==toDoId)
            //.Select(t => new TodoTaskResponseDTO
            //{
            //    Id = t.Id,
            //    Name = t.Name,
            //    Category = t.Category,
            //    Description = t.Description,
            //    CreatedAt = t.CreatedAt,
            //    CreatedBy = t.CreatedBy,
            //    SubmittedBy = t.SubmittedBy,
            //    IsCompleted = t.IsCompleted,
            //    DueDate = t.DueDate,
            //    FinishedDate = t.FinishedDate,
            //    TaskStatus = t.TaskStatus

            //})
            //.FirstOrDefaultAsync();
        }
    }
}
