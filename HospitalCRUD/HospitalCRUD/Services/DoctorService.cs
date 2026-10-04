using HospitalCRUD.Models;
using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Doctor;
using HospitalCRUD.Repositories.IRepositories;
using HospitalCRUD.Services.IServices;

namespace HospitalCRUD.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;
        private readonly IRepository<DoctorSpecialty> _doctorSpecialityRepository;

        public DoctorService(IDoctorRepository repository, IRepository<DoctorSpecialty> doctorSpecialityRepository  )
        {
            _repository = repository;
            _doctorSpecialityRepository = doctorSpecialityRepository;
        }
        //private readonly IRepository<Doctor> _repository;

        //public DoctorService(IRepository<Doctor> repository )
        //{
        //    _repository = repository;
        //}
        public async Task<IEnumerable<DropdownDTO>> GetAllDropdownAsync()
        {
            var dropdown = await _repository.GetAllAsync();

            return dropdown.Select(x => new DropdownDTO
            {
                Id = x.DoctorId,
                Name = x.FirstName+" "+x.LastName
            });
        }
        public async Task<PagedResult<DoctorDTO>> GetAllAsync(PaginationRequest request)
        {
            var dbModels = await _repository.GetAllPaginationWithSpecialtyAsync(
        request.Page,
        request.PageSize,
           request.Search,
        request.SortBy,
        request.SortDirection
    );

            var totalCount = await _repository.GetCountAsync(request.Search);

            return new PagedResult<DoctorDTO>
            {
                Items = dbModels
                    .Select(x => new DoctorDTO
                    {
                        Id = x.DoctorId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        SpecialtyId = x.SpecialtyId,
                         SpecialtyName = x.Specialty.Name
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
        public async Task<DoctorDTO?> GetByIdAsync(int id)
        {
            var dbModel = await _repository.GetByIdWithSpecialtyAsync(id);
            if (dbModel == null)
            {
                return null;
            }

            return new DoctorDTO
            {
                Id = dbModel.DoctorId,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                SpecialtyId = dbModel.SpecialtyId,
                SpecialtyName = dbModel.Specialty.Name

            };
        }

        public async Task<DoctorDTO?> CreateAsync(
    CreateDoctorDTO dto)
        {
            var specialty = await _doctorSpecialityRepository.GetByIdAsync(dto.SpecialtyId);

            if (specialty == null)
            {
                return null;
            }

            var dbModel = new Doctor
            { 

                 FirstName = dto.FirstName,
                LastName = dto.LastName,
                SpecialtyId = dto.SpecialtyId 
           
            };

            await _repository.AddAsync(dbModel);
            await _repository.SaveChangesAsync();
            return new DoctorDTO
            {
                Id = dbModel.DoctorId,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                SpecialtyId = dbModel.SpecialtyId ,
                SpecialtyName=specialty.Name
            };
        }
           // var dbModel = await _repository.GetByIdAsync(id);
        public async Task<DoctorDTO?> UpdateAsync(int id,
    UpdateDoctorDTO dto)
        {
            var dbModel = await _repository.GetByIdWithSpecialtyAsync(id);

            if (dbModel == null)
            {
                return null;
            }
            var specialty = await _doctorSpecialityRepository.GetByIdAsync(dto.SpecialtyId);
            if (specialty == null)
            {
                return null;
            }
            dbModel.FirstName = dto.FirstName;
            dbModel.LastName = dto.LastName;
            dbModel.SpecialtyId = dto.SpecialtyId;
            await _repository.SaveChangesAsync();
            return new DoctorDTO
            {
                Id = dbModel.DoctorId,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                SpecialtyId = dbModel.SpecialtyId,
                SpecialtyName = specialty.Name

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
