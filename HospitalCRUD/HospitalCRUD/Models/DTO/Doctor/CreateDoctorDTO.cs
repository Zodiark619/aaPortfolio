using System.ComponentModel.DataAnnotations;

namespace HospitalCRUD.Models.DTO.Doctor
{
    public class CreateDoctorDTO
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]

        public string LastName { get; set; } = string.Empty;
        [Required]
        
        public int SpecialtyId { get; set; }
    }
}
