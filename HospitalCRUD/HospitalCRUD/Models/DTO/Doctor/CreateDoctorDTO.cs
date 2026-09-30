namespace HospitalCRUD.Models.DTO.Doctor
{
    public class CreateDoctorDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int SpecialtyId { get; set; }
    }
}
