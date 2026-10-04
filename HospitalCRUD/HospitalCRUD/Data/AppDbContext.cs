using HospitalCRUD.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HospitalCRUD.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Doctor> Doctors {  get; set; }
        public DbSet<Province> Provinces {  get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties {  get; set; }
        public DbSet<Patient> Patients {  get; set; }
        public DbSet<Admission> Admissions {  get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Admission>()
       .HasOne(x => x.Patient)
       .WithMany()
       .HasForeignKey(x => x.PatientId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Admission>()
                .HasOne(x => x.AttendingDoctor)
                .WithMany()
                .HasForeignKey(x => x.AttendingDoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Doctor>()
                .HasOne(x => x.Specialty)
                .WithMany()
                .HasForeignKey(x => x.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Patient>()
            .HasOne(x => x.Province)
            .WithMany()
            .HasForeignKey(x => x.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
