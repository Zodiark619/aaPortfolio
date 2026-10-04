using HospitalCRUD.Models;
using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Admission;
using HospitalCRUD.Repositories.IRepositories;
using HospitalCRUD.Services.IServices;

namespace HospitalCRUD.Services
{
    public class AdmissionService:IAdmissionService
    {
        private readonly IAdmissionRepository _repository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AdmissionService(IAdmissionRepository repository, IPatientRepository patientRepository,IDoctorRepository doctorRepository )
        {
            _repository = repository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }
        //private readonly IRepository<Admission> _repository;

        //public AdmissionService(IRepository<Admission> repository )
        //{
        //    _repository = repository;
        //}
        
        public async Task<PagedResult<AdmissionDTO>> GetAllAsync(PaginationRequest request)
        {
            var dbModels = await _repository.GetAllPaginationWithPatientDoctorAsync(
        request.Page,
        request.PageSize,
           request.Search,
        request.SortBy,
        request.SortDirection
    );

            var totalCount = await _repository.GetCountAsync(request.Search);

            return new PagedResult<AdmissionDTO>
            {
                Items = dbModels
                    .Select(x => new AdmissionDTO
                    {
                        AdmissionId = x.AdmissionId,
                         PatientId = x.PatientId,
                         AdmissionDate = x.AdmissionDate,
                         DischargeDate = x.DischargeDate,
                         AttendingDoctorId = x.AttendingDoctorId,
                         Diagnosis=x.Diagnosis,
                         AttendingDoctorName=x.AttendingDoctor.FirstName+" "+ x.AttendingDoctor.LastName,
                         PatientName=x.Patient.FirstName+" "+ x.Patient.LastName,

                    })
                    .ToList(),

                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)request.PageSize
                )
            };
        }
        public async Task<AdmissionDTO?> GetByIdAsync(int id)
        {
            var dbModel = await _repository.GetByIdWithPatientDoctorAsync(id);
            if (dbModel == null)
            {
                return null;
            }

            return new AdmissionDTO
            {
                AdmissionId = dbModel.AdmissionId,
                PatientId = dbModel.PatientId,
                AdmissionDate = dbModel.AdmissionDate,
                DischargeDate = dbModel.DischargeDate,
                AttendingDoctorId = dbModel.AttendingDoctorId,
                Diagnosis=dbModel.Diagnosis,
                AttendingDoctorName = dbModel.AttendingDoctor.FirstName + " " + dbModel.AttendingDoctor.LastName,
                PatientName = dbModel.Patient.FirstName + " " + dbModel.Patient.LastName,


            };
        }

        public async Task<AdmissionDTO?> CreateAsync(
    CreateAdmissionDTO dto)
        {
            var doctor = await _doctorRepository.GetByIdAsync(dto.AttendingDoctorId);
            var patient = await _patientRepository.GetByIdAsync(dto.PatientId);

            if (doctor == null||patient==null)
            {
                return null;
            }

            var dbModel = new Admission
            {

                
                PatientId = dto.PatientId,
                AdmissionDate = dto.AdmissionDate,
                DischargeDate = dto.DischargeDate,
                AttendingDoctorId = dto.AttendingDoctorId,
                Diagnosis=dto.Diagnosis,
                

            };

            await _repository.AddAsync(dbModel);
            await _repository.SaveChangesAsync();
            return new AdmissionDTO
            {
                AdmissionId = dbModel.AdmissionId,
                PatientId = dbModel.PatientId,
                AdmissionDate = dbModel.AdmissionDate,
                DischargeDate = dbModel.DischargeDate,
                AttendingDoctorId = dbModel.AttendingDoctorId,
                Diagnosis=dbModel.Diagnosis,
                AttendingDoctorName = dbModel.AttendingDoctor.FirstName + " " + dbModel.AttendingDoctor.LastName,
                PatientName = dbModel.Patient.FirstName + " " + dbModel.Patient.LastName,
            };
        }
        // var dbModel = await _repository.GetByIdAsync(id);
        public async Task<AdmissionDTO?> UpdateAsync(int id,
    UpdateAdmissionDTO dto)
        {
            var dbModel = await _repository.GetByIdWithPatientDoctorAsync(id);

            if (dbModel == null)
            {
                return null;
            }
            var doctor = await _doctorRepository.GetByIdAsync(dto.AttendingDoctorId);
            var patient = await _patientRepository.GetByIdAsync(dto.PatientId);

            if (doctor == null || patient == null)
            {
                return null;
            }
            dbModel.AdmissionDate = dto.AdmissionDate;
            dbModel.DischargeDate = dto.DischargeDate;
            dbModel.Diagnosis = dto.Diagnosis;
            dbModel.AttendingDoctorId = dto.AttendingDoctorId;
            dbModel.PatientId = dto.PatientId;
            await _repository.SaveChangesAsync();
            return new AdmissionDTO
            {
                AdmissionId = dbModel.AdmissionId,
                PatientId = dbModel.PatientId,
                AdmissionDate = dbModel.AdmissionDate,
                DischargeDate = dbModel.DischargeDate,
                AttendingDoctorId = dbModel.AttendingDoctorId,
                Diagnosis = dbModel.Diagnosis,
                AttendingDoctorName = doctor.FirstName + " " + doctor.LastName,
                PatientName = patient.FirstName + " " + patient.LastName,

            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var dbModel = await _repository.GetByIdAsync(id);

            if (dbModel == null)
            {
                return false;
            }
            _repository.Delete(dbModel);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
