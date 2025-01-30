using Ingrediente_si_Retete.Models;

namespace Ingrediente_si_Retete;

public partial class RecipesPage : ContentPage
{
    public RecipesPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetRecipesAsync();
    }

    async void OnAddRecipeClicked(object sender, EventArgs e)
    {
        nameEditor.Text = string.Empty;
        instructionsEditor.Text = string.Empty;
        prepTimeEditor.Text = string.Empty;
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nameEditor.Text))
        {
            await DisplayAlert("Eroare", "Va rugam introduceti numele retetei", "OK");
            return;
        }

        await App.Database.SaveRecipeAsync(new Recipe
        {
            Name = nameEditor.Text,
            Instructions = instructionsEditor.Text,
            PrepTime = prepTimeEditor.Text
        });

        nameEditor.Text = string.Empty;
        instructionsEditor.Text = string.Empty;
        prepTimeEditor.Text = string.Empty;
        listView.ItemsSource = await App.Database.GetRecipesAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var recipe = listView.SelectedItem as Recipe;
        if (recipe != null)
        {
            await App.Database.DeleteRecipeAsync(recipe);
            listView.ItemsSource = await App.Database.GetRecipesAsync();
            nameEditor.Text = string.Empty;
            instructionsEditor.Text = string.Empty;
            prepTimeEditor.Text = string.Empty;
        }
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var recipe = e.SelectedItem as Recipe;
        if (recipe != null)
        {
            nameEditor.Text = recipe.Name;
            instructionsEditor.Text = recipe.Instructions;
            prepTimeEditor.Text = recipe.PrepTime;
        }
    }
}