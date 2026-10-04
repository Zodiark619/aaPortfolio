using System.ComponentModel.DataAnnotations;

namespace HospitalCRUD.Models.DTO.DoctorSpecialty
{
    public class CreateDoctorSpecialtyDTO
    {
        [Required]

        public string Name { get; set; } = string.Empty;
    }
}
