using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyApp.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        // Tools -> Nuget Package Manager -> Package Manager Consol
        // In Console Type -> add-migration AddCategoryTableToDb
        // It Will automaticaly create Migrations Folder which consists time stamps
        // In Console Type -> update-database
        // It will create database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1,Name="Action",DisplayOrder=1},
                new Category { Id = 2, Name = "Drama", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Sci-Fi", DisplayOrder = 3 }
                );
        }

    }
}
