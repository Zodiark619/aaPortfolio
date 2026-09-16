using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models.DTO
{
    public class TodoTaskResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Category Category { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }
    }
    
}
