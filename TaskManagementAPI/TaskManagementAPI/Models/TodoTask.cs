using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public Category Category { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; } = string.Empty;
        public string? SubmittedBy { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? FinishedDate { get; set; }


    }
    public enum TaskStatus
    {
        Available,Submitted,Approved
    }
    public enum Category
    {
        Work,Personal
    }
}
