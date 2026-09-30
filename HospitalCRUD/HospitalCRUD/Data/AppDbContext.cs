using HospitalCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalCRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Doctor> Doctors {  get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties {  get; set; }
    }
}
