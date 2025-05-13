using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class AdministratorDto
{
    [Required]
    public string? FullName { get; set; } = string.Empty;

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Qualification { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime HireDate { get; set; }

    public IFormFile? ImagePath { get; set; }
}
