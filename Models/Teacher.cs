using System;
using System.ComponentModel.DataAnnotations;

namespace AspNet_school2.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        [StringLength(255, ErrorMessage = "الاسم الكامل يجب أن يكون أقل من 255 حرف")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "رقم المعلم مطلوب")]
        public string TeacherNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "العنوان مطلوب")]
        [StringLength(500, ErrorMessage = "العنوان يجب أن يكون أقل من 500 حرف")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "المادة الدراسية مطلوبة")]
        public string Subject { get; set; } = string.Empty;

        public string? Qualification { get; set; }

        public DateTime HireDate { get; set; }

        public string? ProfileImagePath { get; set; }
    }
}