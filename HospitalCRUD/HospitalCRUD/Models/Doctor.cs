using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalCRUD.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [ForeignKey(nameof(Specialty))]
        public int SpecialtyId { get; set; }
        public DoctorSpecialty Specialty { get; set; } = null!;

    }
     
}
