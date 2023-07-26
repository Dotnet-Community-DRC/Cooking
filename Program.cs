using CookingAPI.Data;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.MapGet("/recipes", (AppDbContext db) => db.Recipes.ToList());

app.MapGet("/recipes/{id}", (AppDbContext db, int id) =>
{
    var recipe = db.Recipes.Find(id);
    if (recipe is null)
        return Results.NotFound($"Recipe with ID {id} not found");
    return Results.Ok(recipe);
});

app.MapPost("/recipes", (AppDbContext db, Recipe recipe) => {
    db.Recipes.Add(recipe);
    db.SaveChanges();
    return Results.Created($"/recipes/{recipe.Id}", recipe);
});

app.MapPut("/recipes/{id}", (AppDbContext db, int id, Recipe recipe) => {
    var existingRecipe = db.Recipes.Find(id);
    
    if (existingRecipe == null) 
        return Results.NotFound($"Recipe with ID {id} not found");
    existingRecipe.Name = recipe.Name;
    existingRecipe.Description = recipe.Description;
    
    db.SaveChanges();
    
    return Results.NoContent();
});

app.MapDelete("/recipes/{id}", (AppDbContext db, int id) => {
    var recipe = db.Recipes.Find(id);
    if (recipe == null) return Results.NotFound($"Recipe with ID {id} not found");
    db.Recipes.Remove(recipe);
    db.SaveChanges();
    return Results.Ok(new { message = "Recipe successfully deleted" });
});

app.Run();
