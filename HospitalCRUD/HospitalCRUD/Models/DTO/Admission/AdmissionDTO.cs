namespace HospitalCRUD.Models.DTO.Admission
{
    public class AdmissionDTO
    {
        public int AdmissionId { get; set; }

        public int PatientId { get; set; }


        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        public int AttendingDoctorId { get; set; }

        public string AttendingDoctorName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
    }
}
