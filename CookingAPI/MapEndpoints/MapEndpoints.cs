using CookingAPI.Models;
using CookingAPI.Services;

namespace CookingAPI.MapEndpoints;

public static class MapEndpoints
{
    public static RouteGroupBuilder MapRecipes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/recipes");
        
        group.MapGet("/", ( IRecipeRepository recipeRepo) => recipeRepo.GetRecipesAsync());

        group.MapGet("/{id:int}", async (IRecipeRepository recipeRepo,int id) =>
        {
            var recipe = await recipeRepo.GetRecipeAsync(id);
            return recipe is null ? Results.NotFound() : Results.Ok(recipe);
        });

        group.MapPost("/", async (IRecipeRepository recipeRepo, Recipe recipe) => {
            await recipeRepo.CreateRecipeAsync(recipe);
            return Results.Created($"/recipes/{recipe.Id}", recipe);
        });

        group.MapPut("/{id}", async (IRecipeRepository recipeRepo, int id, Recipe recipe) =>
        {
            var  recipeToUpdate= await recipeRepo.UpdateRecipeAsync(id, recipe);
            return recipeToUpdate is null ? Results.NotFound($"Recipe with ID {id} not found") :
                Results.Ok(new { message = "Recipe successfully Updated" });
        });

        group.MapDelete("/{id:int}", async (IRecipeRepository recipeRepo, int id) =>
        {
            var recipe = await recipeRepo.DeleteRecipeAsync(id);
            return recipe is null ? Results.NotFound($"Recipe with ID {id} not found") : 
                Results.Ok(new { message = "Recipe successfully deleted" });
        });
        
        return group;
    }
}