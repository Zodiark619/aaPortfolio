using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalCRUD.Models.DTO.Patient
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; } = string.Empty;

       
        public string ProvinceId { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}
