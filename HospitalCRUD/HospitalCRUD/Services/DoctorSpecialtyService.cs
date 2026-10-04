using HospitalCRUD.Models;
using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.DoctorSpecialty;
using HospitalCRUD.Repositories.IRepositories;
using HospitalCRUD.Services.IServices;

namespace HospitalCRUD.Services
{
    public class DoctorSpecialtyService : IDoctorSpecialtyService
    {
        private readonly IDoctorSpecialtyRepository _repository;

        public DoctorSpecialtyService(IDoctorSpecialtyRepository repository)
        {
            _repository = repository;
        }

        
        public async Task<IEnumerable<DropdownDTO>> GetAllDropdownAsync()
        {
            var specialties = await _repository.GetAllAsync();

            return specialties.Select(x => new DropdownDTO
            {
                Id = x.Id,
                Name = x.Name
            });
        }
        public async Task<PagedResult<DoctorSpecialtyDTO>> GetAllAsync(PaginationRequest request)
        {
            var dbModels = await _repository.GetAllPaginationAsync(
        request.Page,
        request.PageSize ,
        request.Search,
        request.SortBy,
        request.SortDirection
    );

            var totalCount = await _repository.GetCountAsync(request.Search);

            return new PagedResult<DoctorSpecialtyDTO>
            {
                Items = dbModels
                    .Select(x => new DoctorSpecialtyDTO
                    {
                        Id = x.Id,
                        Name = x.Name
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
        public async Task<DoctorSpecialtyDTO?> GetByIdAsync(int id)
        {
            var dbModel = await _repository.GetByIdAsync( id  );
            if (dbModel == null)
            {
                return null;
            }
           
            return new  DoctorSpecialtyDTO
            {
                 Id= dbModel.Id,
                 Name = dbModel.Name
                
            };
        }

        public async Task<DoctorSpecialtyDTO> CreateAsync(
    CreateDoctorSpecialtyDTO dto)
        {
            var dbModel = new DoctorSpecialty
            {
                Name = dto.Name
            };

            await _repository.AddAsync(dbModel);
            await _repository.SaveChangesAsync();
            return new DoctorSpecialtyDTO
            {
                Id = dbModel.Id,
                Name = dbModel.Name
            };
        }
        public async Task<DoctorSpecialtyDTO?> UpdateAsync(int id,
    UpdateDoctorSpecialtyDTO dto)
        {
            var dbModel = await _repository.GetByIdAsync(id);

            if (dbModel == null)
            {
                return null;
            }
            dbModel.Name = dto.Name;
            await _repository.SaveChangesAsync();
            return new DoctorSpecialtyDTO
            {
                Id = dbModel.Id,
                Name = dbModel.Name

            };
        }
        public async Task<bool> DeleteAsync( int id)
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
