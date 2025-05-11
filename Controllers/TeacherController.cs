using AspNet_school2.Models;
using AspNet_school2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // إضافة تسجيل الأخطاء

namespace AspNet_school2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger; // إضافة تسجيل الأخطاء

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeachers()
        {
            try
            {
                var teachers = await _teacherService.GetAllTeachersAsync();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all teachers");
                return StatusCode(500, "Internal server error"); // إرجاع خطأ 500
            }
        }

        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);

                if (teacher == null)
                    return NotFound();

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting teacher by id: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Teacher/number/T2023123
        [HttpGet("number/{teacherNumber}")]
        public async Task<ActionResult<Teacher>> GetTeacherByNumber(string teacherNumber)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByNumberAsync(teacherNumber);

                if (teacher == null)
                    return NotFound();

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting teacher by number: {teacherNumber}", teacherNumber);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Teacher
        [HttpPost]
        public async Task<ActionResult<Teacher>> RegisterTeacher([FromBody] TeacherRegistrationDto registrationDto) // إضافة [FromBody]
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // إرجاع أخطاء التحقق من الصحة
            }

            try
            {
                var teacher = await _teacherService.RegisterTeacherAsync(registrationDto);
                return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering teacher");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherRegistrationDto updateDto) // إضافة [FromBody]
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // إرجاع أخطاء التحقق من الصحة
            }

            try
            {
                var success = await _teacherService.UpdateTeacherAsync(id, updateDto);

                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating teacher with id: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var success = await _teacherService.DeleteTeacherAsync(id);

                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher with id: {id}", id);
                return StatusCode(500, "Internal server error");
            }
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

            try
            {
                var success = await _teacherService.UpdateTeacherImageAsync(id, uploadDto.Image);

                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading teacher image for teacher with id: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Teacher/paged?page=1&pageSize=10&search=math
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<Teacher>>> GetPagedTeachers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paged teachers");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}