using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public Category Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        [Required]

        public string UserId { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? FinishedDate { get; set; }
    }
    public enum Category
    {
        Work,Personal
    }
}
