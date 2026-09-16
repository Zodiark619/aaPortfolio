using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models.DTO
{
    public class CreateTodoTaskDTO
    {
        [Required]

        public string Name { get; set; } = string.Empty;
        [Required]

        public Category Category { get; set; }
        public string Description { get; set; }=string.Empty;
       
        public DateTime? DueDate { get; set; }
    }
}
