using HospitalCRUD.Models;

namespace HospitalCRUD.Repositories.IRepositories
{
    public interface IUnitOfWork 
    {
        IRepository<Doctor> Doctors { get; }
        IRepository<DoctorSpecialty> DoctorSpecialties { get; }

        Task<int> SaveChangesAsync();
    }
}
