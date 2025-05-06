using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet_school2.Models;

namespace AspNet_school2.Services
{
    public interface IStudentService
    {
        Task<Student> RegisterStudentAsync(StudentRegistrationDto registrationDto);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> GetStudentByNumberAsync(string studentNumber);
        Task<bool> UpdateStudentAsync(int id, StudentRegistrationDto updateDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}