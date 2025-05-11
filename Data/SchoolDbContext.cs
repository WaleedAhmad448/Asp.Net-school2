using Microsoft.EntityFrameworkCore;
using AspNet_school2.Models;

namespace AspNet_school2.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure Student entity
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentNumber)
                .IsUnique();
                
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();
        }
    }
}