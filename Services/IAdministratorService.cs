using AspNet_school2.Models;

namespace AspNet_school2.Services
 {
    public interface IAdministratorService
    {
        Task<Administrator> CreateAsync(AdministratorDto dto);
        Task<IEnumerable<Administrator>> GetAllAsync();
        Task<Administrator?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, AdministratorDto dto);
        Task<bool> DeleteAsync(int id);
    }

 }

