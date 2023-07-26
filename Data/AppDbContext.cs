using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe { Id = 1, Name = "Sombe wali", Description = "Sweet and common meal eaten in the Central Africa." },
            new Recipe { Id = 2, Name = "Wali Maharagwe", Description = "Rice and beans mostly in the Central African cuisine" }
        );
    }
}