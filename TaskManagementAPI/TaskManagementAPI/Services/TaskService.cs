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

        public TaskService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<TodoTask> CreateTaskAsync(CreateTodoTaskDTO createTaskDTO,string userId)
        {
            var task = new TodoTask
            {
                Name=createTaskDTO.Name,
                Category=createTaskDTO.Category,
                Description=createTaskDTO.Description,
                IsCompleted=false,
                CreatedAt=DateTime.Now,
                DueDate=createTaskDTO.DueDate ?? DateTime.Now.AddDays(7),
                UserId= userId
            };
            _appDbContext.TodoTasks.Add(task);
            await _appDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<TodoTaskPagedResponse> GetAllAsync(
    string userId,
    int page,
    int pageSize)
        {
            var query = _appDbContext.TodoTasks
                .Where(t => t.UserId == userId)
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
                    IsCompleted = t.IsCompleted,
                    DueDate = t.DueDate
                })
                .ToListAsync();

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

        public async Task<TodoTaskResponseDTO?> GetAsync(int toDoId, string userId)
        {
            return await _appDbContext.TodoTasks
        .Where(t => t.Id == toDoId && t.UserId == userId)
        .Select(t => new TodoTaskResponseDTO
        {
            Id = t.Id,
            Name = t.Name,
            Category = t.Category,
            Description = t.Description,
            CreatedAt = t.CreatedAt,
            IsCompleted = t.IsCompleted,
            DueDate = t.DueDate
        })
        .FirstOrDefaultAsync();
        }
    }
}
