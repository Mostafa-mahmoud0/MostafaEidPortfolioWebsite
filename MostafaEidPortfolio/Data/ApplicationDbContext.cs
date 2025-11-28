// In Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using MostafaEidPortfolio.Models;
using MostafaEidPortfolio.Data;
namespace MostafaEidPortfolio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }

        // HERE WE USE FLUENT API INSTEAD OF DATA ANNOTATIONS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Project table
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects"); // Set table name
                entity.HasKey(e => e.Id);   // Set primary key
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired(false); // ImageUrl is optional
                entity.Property(e => e.TechnologiesUsed).HasMaxLength(500);
            });

            // Configure the Service table
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Services"); // Set table name
                entity.HasKey(e => e.Id);   // Set primary key
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
            });
        }
    }
}