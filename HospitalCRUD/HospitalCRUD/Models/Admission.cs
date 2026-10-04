using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalCRUD.Models
{
    public class Admission
    {
        public int AdmissionId { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        public int AttendingDoctorId { get; set; }
        [ForeignKey("AttendingDoctorId")]

        public Doctor AttendingDoctor { get; set; } = null!;
    }
}
