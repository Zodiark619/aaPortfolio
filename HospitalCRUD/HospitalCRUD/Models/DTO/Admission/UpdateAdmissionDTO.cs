namespace HospitalCRUD.Models.DTO.Admission
{
    public class UpdateAdmissionDTO
    {
        public int PatientId { get; set; }


        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        public int AttendingDoctorId { get; set; }
    }
}
