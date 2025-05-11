using AspNet_school2.Models;
using AspNet_school2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_school2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            
            if (teacher == null)
                return NotFound();
                
            return Ok(teacher);
        }

        // GET: api/Teacher/number/T2023123
        [HttpGet("number/{teacherNumber}")]
        public async Task<ActionResult<Teacher>> GetTeacherByNumber(string teacherNumber)
        {
            var teacher = await _teacherService.GetTeacherByNumberAsync(teacherNumber);
            
            if (teacher == null)
                return NotFound();
                
            return Ok(teacher);
        }

        // POST: api/Teacher
        [HttpPost]
        public async Task<ActionResult<Teacher>> RegisterTeacher(TeacherRegistrationDto registrationDto)
        {
            var teacher = await _teacherService.RegisterTeacherAsync(registrationDto);
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
        }

        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherRegistrationDto updateDto)
        {
            var success = await _teacherService.UpdateTeacherAsync(id, updateDto);
            
            if (!success)
                return NotFound();
                
            return NoContent();
        }

        // DELETE: api/Teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var success = await _teacherService.DeleteTeacherAsync(id);
            
            if (!success)
                return NotFound();
                
            return NoContent();
        }
        
        // POST: api/Teacher/5/image
        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadTeacherImage(int id, [FromForm] TeacherImageUploadDto uploadDto)
        {
            if (uploadDto.Image == null || uploadDto.Image.Length == 0)
            {
                return BadRequest("No image file provided");
            }
            
            // Validate file type
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(uploadDto.Image.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file type. Only JPG, JPEG, PNG, and GIF files are allowed.");
            }
            
            // Validate file size (e.g., 5MB max)
            if (uploadDto.Image.Length > 5 * 1024 * 1024)
            {
                return BadRequest("File size exceeds the limit (5MB).");
            }
            
            var success = await _teacherService.UpdateTeacherImageAsync(id, uploadDto.Image);
            
            if (!success)
                return NotFound();
                
            return NoContent();
        }
        
        // GET: api/Teacher/paged?page=1&pageSize=10&search=math
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<Teacher>>> GetPagedTeachers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var (teachers, totalCount) = await _teacherService.GetPagedTeachersAsync(page, pageSize, search);
            
            var result = new PagedResult<Teacher>
            {
                Data = teachers.ToList(),
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalCount
            };
            
            return Ok(result);
        }
    }
}