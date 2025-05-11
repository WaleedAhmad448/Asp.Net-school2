using AspNet_school2.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNet_school2.Services
{
    public interface ITeacherService
    {
        Task<Teacher> RegisterTeacherAsync(TeacherRegistrationDto registrationDto);
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task<Teacher> GetTeacherByNumberAsync(string teacherNumber);
        Task<bool> UpdateTeacherAsync(int id, TeacherRegistrationDto updateDto);
        Task<bool> DeleteTeacherAsync(int id);
        Task<bool> UpdateTeacherImageAsync(int id, IFormFile image);
        Task<(IEnumerable<Teacher> Teachers, int TotalCount)> GetPagedTeachersAsync(int page, int pageSize, string search = "");
    }
}