using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.Doctor;
using HospitalCRUD.Services;
using HospitalCRUD.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {

            var result = await _doctorService.GetAllAsync(paginationRequest);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _doctorService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDTO dto)
        {
            var created = await _doctorService.CreateAsync(dto);
            if (created == null)
            {
                return BadRequest("Specialty does not exist.");
            }
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
           [FromBody] UpdateDoctorDTO dto)
        {
            var updated = await _doctorService.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
