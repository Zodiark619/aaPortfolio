using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalCRUD.Models.DTO.Admission
{
    public class CreateAdmissionDTO
    {
       

        public int PatientId { get; set; }
       

        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        public int AttendingDoctorId { get; set; }
        
    }
}
