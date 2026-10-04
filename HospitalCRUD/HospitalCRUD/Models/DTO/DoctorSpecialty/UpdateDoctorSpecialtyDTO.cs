using System.ComponentModel.DataAnnotations;

namespace HospitalCRUD.Models.DTO.DoctorSpecialty
{
    public class UpdateDoctorSpecialtyDTO
    {
        [Required]

        public string Name { get; set; } = string.Empty;

    }
}
