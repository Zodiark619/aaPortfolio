using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalCRUD.Models.DTO.Doctor
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int SpecialtyId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string? SpecialtyName { get; set; } = string.Empty;
    }
}
