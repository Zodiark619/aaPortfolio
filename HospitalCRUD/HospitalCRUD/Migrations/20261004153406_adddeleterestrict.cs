using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalCRUD.Migrations
{
    /// <inheritdoc />
    public partial class adddeleterestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Doctors_AttendingDoctorId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Provinces_ProvinceId",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "DoctorSpecialtyId",
                table: "Doctors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorSpecialtyId",
                table: "Doctors",
                column: "DoctorSpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Doctors_AttendingDoctorId",
                table: "Admissions",
                column: "AttendingDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_DoctorSpecialtyId",
                table: "Doctors",
                column: "DoctorSpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Provinces_ProvinceId",
                table: "Patients",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Doctors_AttendingDoctorId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_DoctorSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Provinces_ProvinceId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DoctorSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorSpecialtyId",
                table: "Doctors");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Doctors_AttendingDoctorId",
                table: "Admissions",
                column: "AttendingDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Provinces_ProvinceId",
                table: "Patients",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
