using HospitalCRUD.Models;
using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Province;
using HospitalCRUD.Repositories.IRepositories;
using HospitalCRUD.Services.IServices;

namespace HospitalCRUD.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _repository;

        public ProvinceService(IProvinceRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<ProvinceDropdownDTO>> GetAllDropdownAsync()
        {
            var provinces = await _repository.GetAllAsync();

            return provinces.Select(x => new ProvinceDropdownDTO
            {
                Id= x.ProvinceId,
                Name = x.ProvinceName
            });
        }
        public async Task<PagedResult<ProvinceDTO>> GetAllAsync(PaginationRequest request)
        {
            var dbModels = await _repository.GetAllPaginationAsync(
        request.Page,
        request.PageSize,
        request.Search,
        request.SortBy,
        request.SortDirection
    );

            var totalCount = await _repository.GetCountAsync(request.Search);

            return new PagedResult<ProvinceDTO>
            {
                Items = dbModels
                    .Select(x => new ProvinceDTO
                    {
                        ProvinceId = x.ProvinceId,
                        ProvinceName = x.ProvinceName
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
        public async Task<ProvinceDTO?> GetByIdAsync(string id)
        {
            var dbModel = await _repository.GetByStringIdAsync(id);
            if (dbModel == null)
            {
                return null;
            }

            return new ProvinceDTO
            {
                ProvinceId = dbModel.ProvinceId,
                ProvinceName = dbModel.ProvinceName

            };
        }

        public async Task<ProvinceDTO> CreateAsync(
    CreateProvinceDTO dto)
        {
            var dbModel = new Province
            {
                ProvinceId = dto.ProvinceId,
                ProvinceName = dto.ProvinceName
            };

            await _repository.AddAsync(dbModel);
            await _repository.SaveChangesAsync();
            return new ProvinceDTO
            {
                ProvinceId = dbModel.ProvinceId,
                ProvinceName = dbModel.ProvinceName
            };
        }
        public async Task<ProvinceDTO?> UpdateAsync(string id,
    UpdateProvinceDTO dto)
        {
            var dbModel = await _repository.GetByStringIdAsync(id);

            if (dbModel == null)
            {
                return null;
            }
            dbModel.ProvinceName = dto.ProvinceName;
            await _repository.SaveChangesAsync();
            return new ProvinceDTO
            {
                ProvinceId = dbModel.ProvinceId,
                ProvinceName = dbModel.ProvinceName

            };
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var dbModel = await _repository.GetByStringIdAsync(id);

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
