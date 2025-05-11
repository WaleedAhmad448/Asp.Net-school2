using System;
using System.ComponentModel.DataAnnotations;

namespace AspNet_school2.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public string TeacherNumber { get; set; } = string.Empty;
        
        [Required]
        public string Address { get; set; } = string.Empty;
        
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Subject { get; set; } = string.Empty;
        
        public string? Qualification { get; set; }
        
        public DateTime HireDate { get; set; }
        
        public string? ProfileImagePath { get; set; }
    }
}