using AspNet_school2.Data;
using AspNet_school2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_school2.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolDbContext _context;
        private readonly IFileStorageService _fileStorage;
        private readonly ILogger<TeacherService> _logger; // إضافة تسجيل الأخطاء

        public TeacherService(SchoolDbContext context, IFileStorageService fileStorage, ILogger<TeacherService> logger)
        {
            _context = context;
            _fileStorage = fileStorage;
            _logger = logger;
        }

        public async Task<Teacher> RegisterTeacherAsync(TeacherRegistrationDto registrationDto)
        {
            try
            {
                // Generate a unique teacher number
                string teacherNumber = await GenerateUniqueTeacherNumberAsync();

                // Create a new teacher from the registration DTO
                var teacher = new Teacher
                {
                    FullName = registrationDto.FullName,
                    DateOfBirth = registrationDto.DateOfBirth,
                    TeacherNumber = teacherNumber,
                    Address = registrationDto.Address,
                    PhoneNumber = registrationDto.PhoneNumber,
                    Email = registrationDto.Email,
                    Subject = registrationDto.Subject,
                    Qualification = registrationDto.Qualification,
                    HireDate = DateTime.Now
                };

                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();

                return teacher;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering teacher");
                throw; // إعادة رمي الخطأ ليتم التعامل معه في مكان آخر (مثل الـ Controller)
            }
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<Teacher> GetTeacherByNumberAsync(string teacherNumber)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(t => t.TeacherNumber == teacherNumber);
        }

        public async Task<bool> UpdateTeacherAsync(int id, TeacherRegistrationDto updateDto)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);

                if (teacher == null)
                    return false;

                teacher.FullName = updateDto.FullName;
                teacher.DateOfBirth = updateDto.DateOfBirth;
                teacher.Address = updateDto.Address;
                teacher.PhoneNumber = updateDto.PhoneNumber;
                teacher.Email = updateDto.Email;
                teacher.Subject = updateDto.Subject;
                teacher.Qualification = updateDto.Qualification;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating teacher with id: {id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);

                if (teacher == null)
                    return false;

                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher with id: {id}", id);
                throw;
            }
        }

        public async Task<bool> UpdateTeacherImageAsync(int id, IFormFile image)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);

                if (teacher == null)
                    return false;

                // Delete old image if exists
                if (!string.IsNullOrEmpty(teacher.ProfileImagePath))
                {
                    try
                    {
                        _fileStorage.DeleteFile(teacher.ProfileImagePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error deleting old profile image for teacher with id: {id}", id);
                        // لا ترمي الخطأ هنا، استمر في العملية
                    }
                }

                // Save new image
                string imagePath = await _fileStorage.SaveFileAsync(image);
                teacher.ProfileImagePath = imagePath;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating teacher image for teacher with id: {id}", id);
                throw;
            }
        }

        public async Task<(IEnumerable<Teacher> Teachers, int TotalCount)> GetPagedTeachersAsync(int page, int pageSize, string search = "")
        {
            IQueryable<Teacher> query = _context.Teachers;

            // Apply search if provided
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t =>
                    t.FullName.Contains(search) ||
                    t.Email.Contains(search) ||
                    t.Subject.Contains(search) ||
                    t.TeacherNumber.Contains(search));
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Apply pagination
            var teachers = await query
                .OrderByDescending(t => t.HireDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (teachers, totalCount);
        }

        private async Task<string> GenerateUniqueTeacherNumberAsync()
        {
            string teacherNumber;
            do
            {
                // Generate a teacher number in format: T + Year + Random 3-digit number
                string year = DateTime.Now.Year.ToString();
                string randomDigits = new Random().Next(100, 999).ToString();

                teacherNumber = $"T{year}{randomDigits}";
            } while (await _context.Teachers.AnyAsync(t => t.TeacherNumber == teacherNumber)); // التأكد من عدم وجود رقم مماثل

            return teacherNumber;
        }
    }
}