using System;
using System.ComponentModel.DataAnnotations;

namespace AspNet_school2.Models
{
    public class StudentRegistrationDto
    {
        [Required]
        [StringLength(100)]
        public required string FullName { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [StringLength(100)]
        public required string Address { get; set; }
        
        [Required]
        [StringLength(15)]
        public required string PhoneNumber { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }
        
        [Required]
        public int GradeLevel { get; set; }
        
        [StringLength(200)]
        public string? ParentName { get; set; }
        
        [StringLength(15)]
        public string? ParentPhoneNumber { get; set; }
    }
}