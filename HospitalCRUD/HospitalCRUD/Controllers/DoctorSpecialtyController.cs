using HospitalCRUD.Models;
using HospitalCRUD.Models.DTO;
using HospitalCRUD.Models.DTO.DoctorSpecialty;
using HospitalCRUD.Repositories.IRepositories;
using HospitalCRUD.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HospitalCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecialtyController : ControllerBase
    {
        private readonly IDoctorSpecialtyService _doctorSpecialtyService; 

        public DoctorSpecialtyController(IDoctorSpecialtyService doctorSpecialtyService )
        {
            _doctorSpecialtyService = doctorSpecialtyService; 
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetAllDropdown( )
        {

            var result = await _doctorSpecialtyService.GetAllDropdownAsync();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]PaginationRequest paginationRequest)
        {

            var result = await _doctorSpecialtyService.GetAllAsync(paginationRequest);

            return Ok(result);
        }
       
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _doctorSpecialtyService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorSpecialtyDTO dto)
        {
            var created=await _doctorSpecialtyService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        } 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
           [FromBody] UpdateDoctorSpecialtyDTO dto)
        {
            var updated = await _doctorSpecialtyService.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorSpecialtyService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var specialties = await _repository.GetAllAsync();

        //    return Ok(specialties);
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var specialty = await _repository.GetByIdAsync(id);

        //    if (specialty == null)
        //        return NotFound();

        //    return Ok(specialty);
        //}

        // POST: api/doctorspecialty

        //[HttpPost]
        //public async Task<IActionResult> Create(DoctorSpecialty specialty)
        //{
        //    await _repository.AddAsync(specialty);
        //    await _repository.SaveChangesAsync();

        //    return CreatedAtAction(
        //        nameof(GetById),
        //        new { id = specialty.Id },
        //        specialty
        //    );
        //}

        //// PUT: api/doctorspecialty/1
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(
        //    int id,
        //    DoctorSpecialty specialty)
        //{
        //    var existing = await _repository.GetByIdAsync(id);

        //    if (existing == null)
        //        return NotFound();

        //    existing.Name = specialty.Name;

        //    _repository.Update(existing);
        //    await _repository.SaveChangesAsync();

        //    return NoContent();
        //}

        //// DELETE: api/doctorspecialty/1
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var specialty = await _repository.GetByIdAsync(id);

        //    if (specialty == null)
        //        return NotFound();

        //    _repository.Delete(specialty);
        //    await _repository.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
