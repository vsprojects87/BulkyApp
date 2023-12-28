using BulkyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyApp.Data
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

        
    }
}
