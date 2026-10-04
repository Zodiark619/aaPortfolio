using System.ComponentModel.DataAnnotations;

namespace HospitalCRUD.Models.DTO.Patient
{
    public class CreatePatientDTO
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]

        public string LastName { get; set; } = string.Empty;
        [Required]

        public string Gender { get; set; } = string.Empty;
        [Required]

        public DateTime DateOfBirth { get; set; }
        [Required]

        public string City { get; set; } = string.Empty;

        [Required]

        public string ProvinceId { get; set; } = string.Empty;
        [Required]

        public string Allergies { get; set; } = string.Empty;
        [Required]

        public double Height { get; set; }
        [Required]

        public double Weight { get; set; }
    }
}
