using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Patient;
using HospitalCRUD.Services;
using HospitalCRUD.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _PatientService;

        public PatientController(IPatientService PatientService)
        {
            _PatientService = PatientService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetAllDropdown()
        {

            var result = await _PatientService.GetAllDropdownAsync();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {

            var result = await _PatientService.GetAllAsync(paginationRequest);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _PatientService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDTO dto)
        {
            var created = await _PatientService.CreateAsync(dto);
            if (created == null)
            {
                return BadRequest("Specialty does not exist.");
            }
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.PatientId },
                created
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
           [FromBody] UpdatePatientDTO dto)
        {
            var updated = await _PatientService.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _PatientService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
