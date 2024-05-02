using Bulky.Models;
using BulkyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyApp.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }


		// Tools -> Nuget Package Manager -> Package Manager Consol
		// In Console Type -> add-migration AddCategoryTableToDb
		// It Will automaticaly create Migrations Folder which consists time stamps
		// In Console Type -> update-database
		// It will create database

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1,Name="Action",DisplayOrder=1},
                new Category { CategoryId = 2, Name = "Drama", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Sci-Fi", DisplayOrder = 3 }
                );


			modelBuilder.Entity<Product>().HasData(
				new Product
                {
                    Id=1,
                    Title="Fortune of Time",
                    Author="Billy Spark",
                    Description="xyz",
                    ISBN= "SWD9999001",
                    ListPrice=99,
                    Price=90,
                    Price50=85,
                    Price100=80,
                    CategoryId=1,
                    ImgUrl=""
                },

				new Product
				{
					Id = 2,
					Title = "How to Win Friends and Influence People",
					Author = "Dale Carnegie",
					Description = "xyz",
					ISBN = "SWD9999002",
					ListPrice = 199,
					Price = 190,
					Price50 = 185,
					Price100 = 180,
                    CategoryId=2,
                    ImgUrl = ""
                }

				);
		}

    }
}
