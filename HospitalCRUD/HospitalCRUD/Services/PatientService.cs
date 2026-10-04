using HospitalCRUD.Models;
using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Patient;
using HospitalCRUD.Repositories.IRepositories;
using HospitalCRUD.Services.IServices;

namespace HospitalCRUD.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IProvinceRepository _provinceRepository;

        public PatientService(IPatientRepository repository, IProvinceRepository provinceRepository)
        {
            _repository = repository;
            _provinceRepository = provinceRepository;
        }
        //private readonly IRepository<Patient> _repository;

        //public PatientService(IRepository<Patient> repository )
        //{
        //    _repository = repository;
        //}
        public async Task<IEnumerable<DropdownDTO>> GetAllDropdownAsync()
        {
            var dropdown = await _repository.GetAllAsync();

            return dropdown.Select(x => new DropdownDTO
            {
                Id = x.PatientId,
                Name = x.FirstName + " " + x.LastName
            });
        }
        public async Task<PagedResult<PatientDTO>> GetAllAsync(PaginationRequest request)
        {
            var dbModels = await _repository.GetAllPaginationWithProvinceAsync(
        request.Page,
        request.PageSize,
           request.Search,
        request.SortBy,
        request.SortDirection
    );

            var totalCount = await _repository.GetCountAsync(request.Search);

            return new PagedResult<PatientDTO>
            {
                Items = dbModels
                    .Select(x => new PatientDTO
                    {
                        PatientId = x.PatientId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Gender = x.Gender,
                        DateOfBirth = x.DateOfBirth,
                        City = x.City,
                          ProvinceId = x.ProvinceId,
                        ProvinceName = x.Province.ProvinceName,
                        Allergies = x.Allergies,
                        Height = x.Height,
                        Weight = x.Weight,

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
        public async Task<PatientDTO?> GetByIdAsync(int id)
        {
            var dbModel = await _repository.GetByIdWithProvinceAsync(id);
            if (dbModel == null)
            {
                return null;
            }

            return new PatientDTO
            {
                PatientId = dbModel.PatientId,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                Gender = dbModel.Gender,
                DateOfBirth = dbModel.DateOfBirth,
                City = dbModel.City,
                ProvinceId = dbModel.ProvinceId,
                ProvinceName = dbModel.Province.ProvinceName,
                Allergies = dbModel.Allergies,
                Height = dbModel.Height,
                Weight = dbModel.Weight,

            };
        }

        public async Task<PatientDTO?> CreateAsync(
    CreatePatientDTO dto)
        {
            var province = await _provinceRepository.GetByStringIdAsync(dto.ProvinceId);

            if (province == null)
            {
                return null;
            }

            var dbModel = new Patient
            {

                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                City = dto.City,
                ProvinceId = dto.ProvinceId,
                Allergies = dto.Allergies,
                Height = dto.Height,
                Weight = dto.Weight,

            };

            await _repository.AddAsync(dbModel);
            await _repository.SaveChangesAsync();
            return new PatientDTO
            {
                PatientId = dbModel.PatientId,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                Gender = dbModel.Gender,
                DateOfBirth = dbModel.DateOfBirth,
                City = dbModel.City,
                ProvinceId = dbModel.ProvinceId,
                ProvinceName = province.ProvinceName,
                Allergies = dbModel.Allergies,
                Height = dbModel.Height,
                Weight = dbModel.Weight,
            };
        }
        // var dbModel = await _repository.GetByIdAsync(id);
        public async Task<PatientDTO?> UpdateAsync(int id,
    UpdatePatientDTO dto)
        {
            var dbModel = await _repository.GetByIdWithProvinceAsync(id);

            if (dbModel == null)
            {
                return null;
            }
            var province = await _provinceRepository.GetByStringIdAsync(dto.ProvinceId);
            if (province == null)
            {
                return null;
            }
            dbModel.FirstName = dto.FirstName;
            dbModel.LastName = dto.LastName;
            dbModel.ProvinceId = dto.ProvinceId;
            await _repository.SaveChangesAsync();
            return new PatientDTO
            {
                PatientId = dbModel.PatientId,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                Gender = dbModel.Gender,
                DateOfBirth = dbModel.DateOfBirth,
                City = dbModel.City,
                ProvinceId = dbModel.ProvinceId,
                ProvinceName = province.ProvinceName,
                Allergies = dbModel.Allergies,
                Height = dbModel.Height,
                Weight = dbModel.Weight,

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
