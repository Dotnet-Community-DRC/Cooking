using CookingAPI.Data;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Services;

public class RecipeRepository: IRecipeRepository
{
    private readonly AppDbContext _db;
    public RecipeRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task<List<Recipe>> GetRecipesAsync()
    {
        return await _db.Recipes.ToListAsync();
    }
    public async Task<Recipe> GetRecipeAsync(int id)
    {
        return  await _db.Recipes.FindAsync(id);
    }
    public async Task CreateRecipeAsync(Recipe recipe)
    {
        await _db.Recipes.AddAsync(recipe);
        await _db.SaveChangesAsync();
    }
    public async Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe)
    {
        var existingRecipe = await _db.Recipes.FindAsync(id);
        if (existingRecipe is not null)
        {
            existingRecipe.Name = recipe.Name;
            existingRecipe.Description = recipe.Description;
        }
            
        await _db.SaveChangesAsync();

        return existingRecipe;
    }

    public async Task<Recipe> DeleteRecipeAsync(int id)
    {
        var recipe = await _db.Recipes.FindAsync(id);
        if (recipe != null)
            _db.Recipes.Remove(recipe);
        await _db.SaveChangesAsync();

        return recipe;
    }
}