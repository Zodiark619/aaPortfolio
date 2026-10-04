using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Province;
using HospitalCRUD.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _ProvinceService;

        public ProvinceController(IProvinceService ProvinceService)
        {
            _ProvinceService = ProvinceService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetAllDropdown()
        {

            var result = await _ProvinceService.GetAllDropdownAsync();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {

            var result = await _ProvinceService.GetAllAsync(paginationRequest);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _ProvinceService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProvinceDTO dto)
        {
            var created = await _ProvinceService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.ProvinceId },
                created
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            string id,
           [FromBody] UpdateProvinceDTO dto)
        {
            var updated = await _ProvinceService.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _ProvinceService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
