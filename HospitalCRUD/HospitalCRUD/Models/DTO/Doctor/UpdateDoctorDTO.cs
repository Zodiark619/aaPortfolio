namespace HospitalCRUD.Models.DTO.Doctor
{
    public class UpdateDoctorDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int SpecialtyId { get; set; }
    }
}
