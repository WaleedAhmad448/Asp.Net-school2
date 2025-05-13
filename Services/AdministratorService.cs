using AspNet_school2.Data;
using AspNet_school2.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNet_school2.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdministratorService(SchoolDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<Administrator> CreateAsync(AdministratorDto dto)
        {
            string filePath = await SaveImagePathAsync(dto.ImagePath);
            
            var admin = new Administrator
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Qualification = dto.Qualification,
                HireDate = dto.HireDate,
                DateOfBirth = dto.DateOfBirth,
                AdministratorNumber = GenerateAdministratorNumber(),
                ImagePath = filePath
            };

            _context.Administrators.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<IEnumerable<Administrator>> GetAllAsync()
        {
            return await _context.Administrators.ToListAsync();
        }

        public async Task<Administrator?> GetByIdAsync(int id)
        {
            return await _context.Administrators.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, AdministratorDto dto)
        {
            var admin = await _context.Administrators.FindAsync(id);
            if (admin == null) return false;

            admin.FullName = dto.FullName;
            admin.Email = dto.Email;
            admin.PhoneNumber = dto.PhoneNumber;
            admin.Address = dto.Address;
            admin.Qualification = dto.Qualification;
            admin.HireDate = dto.HireDate;
            admin.DateOfBirth = dto.DateOfBirth;

            if (dto.ImagePath != null)
            {
                string filePath = await SaveImagePathAsync(dto.ImagePath);
                admin.ImagePath = filePath;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var admin = await _context.Administrators.FindAsync(id);
            if (admin == null) return false;

            _context.Administrators.Remove(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateAdministratorNumber()
        {
            return $"ADM{DateTime.Now.Year}{new Random().Next(1000, 9999)}";
        }

        private async Task<string> SaveImagePathAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0) return string.Empty;

            string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "administrators");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // return relative path to use in front-end
            return Path.Combine("uploads", "administrators", uniqueFileName).Replace("\\", "/");
        }
    }
}
