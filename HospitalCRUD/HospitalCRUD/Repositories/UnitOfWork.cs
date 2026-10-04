using HospitalCRUD.Data;
using HospitalCRUD.Models;
using HospitalCRUD.Repositories.IRepositories;

namespace HospitalCRUD.Repositories
{
    public class UnitOfWork  : IUnitOfWork 
    {
        private readonly AppDbContext _context;

        public IRepository<Doctor> Doctors { get; }
        public IRepository<DoctorSpecialty> DoctorSpecialties { get; }
        public IRepository<Province> Provinces { get; }
        public IRepository<Patient> Patients { get; }

        public UnitOfWork(
            AppDbContext context,
            IRepository<Doctor> doctors ,
            IRepository<DoctorSpecialty> doctorSpecialties,
            IRepository<Province> provinces,
            IRepository<Patient> patients
            )
        {
            _context = context;
            Doctors = doctors;
            DoctorSpecialties = doctorSpecialties;
            Provinces = provinces;
            Patients = patients;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
