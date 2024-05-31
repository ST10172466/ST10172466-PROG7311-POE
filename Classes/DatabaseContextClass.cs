using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Classes
{
	public class DatabaseContextClass : DbContext
	{
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Database set that stores all the variables the Model Classes
		/// </summary>
		//public virtual DbSet<RegisterModel> Users { get; set; }
		public virtual DbSet<UserModel> Users { get; set; }
		public virtual DbSet<RoleModel> Roles { get; set; }
		public virtual DbSet<ProductModel> Product { get; set; }
		public virtual DbSet<CategoryModel> Category { get; set; }

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor implements the connection string from the App.config file
		/// </summary>
		public DatabaseContextClass(DbContextOptions<DatabaseContextClass> options) : base(options)
		{

		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to populate the database
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Populates the database with the models
			modelBuilder.Entity<UserModel>()
			   .HasKey(e => e.UserID);
			modelBuilder.Entity<RoleModel>()
			   .HasKey(e => e.RoleID);
			modelBuilder.Entity<ProductModel>()
				.HasKey(e => e.ProductID);
			modelBuilder.Entity<CategoryModel>()
				.HasKey(e => e.CategoryID);

			// Defines the relationships between the tables

			// User to Role: Many-to-one
			modelBuilder.Entity<UserModel>()
				.HasOne(u => u.Role)
				.WithMany(r => r.Users)
				.HasForeignKey(u => u.RoleID);

			// Product to User: Many-to-one
			modelBuilder.Entity<ProductModel>()
				.HasOne(p => p.User)
				.WithMany(u => u.Products)
				.HasForeignKey(p => p.UserID);

			// Product to Category: Many-to-one
			modelBuilder.Entity<ProductModel>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryID);
		}

		//-----------------------------------------------------------------------------------------------//
	}
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//