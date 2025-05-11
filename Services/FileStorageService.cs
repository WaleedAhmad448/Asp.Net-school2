using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNet_school2.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadsFolder;

        public FileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
            _uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "students");
            
            // التأكد من وجود المجلد
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }

            // إنشاء اسم فريد للملف
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            string filePath = Path.Combine(_uploadsFolder, fileName);

            // حفظ الملف
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // إرجاع المسار النسبي للملف
            return $"/uploads/students/{fileName}";
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            // استخراج اسم الملف من المسار
            string fileName = Path.GetFileName(filePath);
            string fullPath = Path.Combine(_uploadsFolder, fileName);

            // حذف الملف إذا كان موجودًا
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFile(string filePath);
    }
}