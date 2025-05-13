using AspNet_school2.Models;
using AspNet_school2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNet_school2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        // POST: api/Administrator
        [HttpPost]
        public async Task<ActionResult<Administrator>> CreateAdministrator([FromForm] AdministratorDto dto)
        {
            var created = await _administratorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAdministratorById), new { id = created.Id }, created);
        }

        // GET: api/Administrator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> GetAllAdministrators()
        {
            var admins = await _administratorService.GetAllAsync();
            return Ok(admins);
        }

        // GET: api/Administrator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> GetAdministratorById(int id)
        {
            var admin = await _administratorService.GetByIdAsync(id);
            if (admin == null)
                return NotFound();
            return Ok(admin);
        }

        // PUT: api/Administrator/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdministrator(int id, [FromForm] AdministratorDto dto)
        {
            var success = await _administratorService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/Administrator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrator(int id)
        {
            var success = await _administratorService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
