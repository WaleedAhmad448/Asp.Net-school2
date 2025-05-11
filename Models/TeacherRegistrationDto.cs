using System;
using System.ComponentModel.DataAnnotations;

namespace AspNet_school2.Models
{
    public class TeacherRegistrationDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
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
    }
}