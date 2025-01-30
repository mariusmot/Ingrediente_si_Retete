using Ingrediente_si_Retete.Models;

namespace Ingrediente_si_Retete;

public partial class RecipeFinder : ContentPage
{
    // O clasa helper pentru a gestiona selectia ingredientelor
    public class SelectableIngredient
    {
        public Ingredient Ingredient { get; set; }
        public bool IsSelected { get; set; }
        public string Name => Ingredient.Name;
    }

    private List<SelectableIngredient> _ingredients;

    public RecipeFinder()
    {
        InitializeComponent();
        LoadIngredients();
    }

    private async void LoadIngredients()
    {
        var allIngredients = await App.Database.GetIngredientsAsync();
        _ingredients = allIngredients.Select(i => new SelectableIngredient
        {
            Ingredient = i,
            IsSelected = false
        }).ToList();

        ingredientsList.ItemsSource = _ingredients;
    }

    async void OnSearchClicked(object sender, EventArgs e)
    {
        var selectedIngredientIds = _ingredients
            .Where(i => i.IsSelected)
            .Select(i => i.Ingredient.ID)
            .ToList();

        if (!selectedIngredientIds.Any())
        {
            await DisplayAlert("Atentie", "Selectati cel putin un ingredient", "OK");
            return;
        }

        var recipes = await App.Database.FindRecipesByIngredientsAsync(selectedIngredientIds);
        recipesList.ItemsSource = recipes;

        if (!recipes.Any())
        {
            await DisplayAlert("Info", "Nu s-au gasit retete cu ingredientele selectate", "OK");
        }
    }
}