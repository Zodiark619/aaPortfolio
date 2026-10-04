using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Admission;
using HospitalCRUD.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _AdmissionService;

        public AdmissionController(IAdmissionService AdmissionService)
        {
            _AdmissionService = AdmissionService;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {

            var result = await _AdmissionService.GetAllAsync(paginationRequest);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _AdmissionService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdmissionDTO dto)
        {
            var created = await _AdmissionService.CreateAsync(dto);
            if (created == null)
            {
                return BadRequest("Admission does not exist.");
            }
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.AdmissionId },
                created
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
           [FromBody] UpdateAdmissionDTO dto)
        {
            var updated = await _AdmissionService.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _AdmissionService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
