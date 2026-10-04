using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IUnitOfWork 
    {
        IRepository<Doctor> Doctors { get; }
        IRepository<DoctorSpecialty> DoctorSpecialties { get; }
        IRepository<Province> Provinces { get; }
        IRepository<Patient> Patients { get; }

        Task<int> SaveChangesAsync();
    }
}
