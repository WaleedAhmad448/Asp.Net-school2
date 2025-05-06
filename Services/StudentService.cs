using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet_school2.Data;
using AspNet_school2.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNet_school2.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;

        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Student> RegisterStudentAsync(StudentRegistrationDto registrationDto)
        {
            // Generate a unique student number (you can customize this logic)
            string studentNumber = GenerateStudentNumber();
            
            // Create a new student from the registration DTO
            var student = new Student
            {
                FullName = registrationDto.FullName,
                DateOfBirth = registrationDto.DateOfBirth,
                StudentNumber = studentNumber,
                Address = registrationDto.Address,
                PhoneNumber = registrationDto.PhoneNumber,
                Email = registrationDto.Email,
                GradeLevel = registrationDto.GradeLevel,
                ParentName = registrationDto.ParentName,
                ParentPhoneNumber = registrationDto.ParentPhoneNumber,
                RegistrationDate = DateTime.Now
            };
            
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            
            return student;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> GetStudentByNumberAsync(string studentNumber)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);
        }

        public async Task<bool> UpdateStudentAsync(int id, StudentRegistrationDto updateDto)
        {
            var student = await _context.Students.FindAsync(id);
            
            if (student == null)
                return false;
                
            student.FullName = updateDto.FullName;
            student.DateOfBirth = updateDto.DateOfBirth;
            student.Address = updateDto.Address;
            student.PhoneNumber = updateDto.PhoneNumber;
            student.Email = updateDto.Email;
            student.GradeLevel = updateDto.GradeLevel;
            student.ParentName = updateDto.ParentName;
            student.ParentPhoneNumber = updateDto.ParentPhoneNumber;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            
            if (student == null)
                return false;
                
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
        
        private string GenerateStudentNumber()
        {
            // Generate a student number in format: Year + Random 4-digit number
            string year = DateTime.Now.Year.ToString();
            string randomDigits = new Random().Next(1000, 9999).ToString();
            
            return $"{year}{randomDigits}";
        }
    }
}