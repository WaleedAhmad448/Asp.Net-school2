using Microsoft.AspNetCore.Http;

namespace AspNet_school2.Models
{
    public class TeacherImageUploadDto
    {
        public IFormFile Image { get; set; } = null!;
    }
}