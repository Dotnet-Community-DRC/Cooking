using CookingAPI.Models;

namespace CookingAPI.Services;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetRecipesAsync();
    Task<Recipe> GetRecipeAsync(int id);
    Task CreateRecipeAsync(Recipe recipe);
    Task<Recipe> UpdateRecipeAsync(int id, Recipe recipe);
    Task<Recipe> DeleteRecipeAsync(int id);
}