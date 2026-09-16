using TaskManagementAPI.Models.DTO;

namespace TaskManagementAPI.Models
{
    public class TodoTaskPagedResponse
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public IEnumerable<TodoTaskResponseDTO> Items { get; set; }
            = Enumerable.Empty<TodoTaskResponseDTO>();
    }
}
