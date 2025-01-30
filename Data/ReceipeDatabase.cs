using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ingrediente_si_Retete.Models;

namespace Ingrediente_si_Retete.Data
{
    public class RecipeDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public RecipeDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<Ingredient>().Wait();
            _database.CreateTableAsync<RecipeIngredient>().Wait();
        }

        // Operatii pentru Retete
        public Task<List<Recipe>> GetRecipesAsync()
        {
            return _database.Table<Recipe>().ToListAsync();
        }

        public Task<Recipe> GetRecipeAsync(int id)
        {
            return _database.Table<Recipe>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecipeAsync(Recipe recipe)
        {
            if (recipe.ID != 0)
                return _database.UpdateAsync(recipe);
            else
                return _database.InsertAsync(recipe);
        }

        public Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            return _database.DeleteAsync(recipe);
        }

        // Operatii pentru Ingrediente
        public Task<List<Ingredient>> GetIngredientsAsync()
        {
            return _database.Table<Ingredient>().ToListAsync();
        }

        public Task<int> SaveIngredientAsync(Ingredient ingredient)
        {
            if (ingredient.ID != 0)
                return _database.UpdateAsync(ingredient);
            else
                return _database.InsertAsync(ingredient);
        }

        public Task<int> DeleteIngredientAsync(Ingredient ingredient)
        {
            return _database.DeleteAsync(ingredient);
        }

        // Cautare retete dupa ingrediente
        public Task<List<Recipe>> FindRecipesByIngredientsAsync(List<int> ingredientIds)
        {
            if (!ingredientIds.Any())
                return Task.FromResult(new List<Recipe>());

            var questionMarks = string.Join(",", ingredientIds.Select(_ => "?"));
            return _database.QueryAsync<Recipe>(
                "SELECT DISTINCT R.* FROM Recipe R " +
                "INNER JOIN RecipeIngredient RI ON R.ID = RI.RecipeID " +
                "WHERE RI.IngredientID IN (" + questionMarks + ")",
                ingredientIds.Cast<object>().ToArray());
        }
    }
}