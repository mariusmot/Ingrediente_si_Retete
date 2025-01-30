using Ingrediente_si_Retete.Models;

namespace Ingrediente_si_Retete;

public partial class IngredientsPage : ContentPage
{
    public IngredientsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetIngredientsAsync();
    }

    async void OnAddIngredientClicked(object sender, EventArgs e)
    {
        ingredientEditor.Text = string.Empty;
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ingredientEditor.Text))
        {
            await DisplayAlert("Eroare", "Va rugam introduceti numele ingredientului", "OK");
            return;
        }

        await App.Database.SaveIngredientAsync(new Ingredient
        {
            Name = ingredientEditor.Text
        });

        ingredientEditor.Text = string.Empty;
        listView.ItemsSource = await App.Database.GetIngredientsAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var ingredient = listView.SelectedItem as Ingredient;
        if (ingredient != null)
        {
            await App.Database.DeleteIngredientAsync(ingredient);
            listView.ItemsSource = await App.Database.GetIngredientsAsync();
            ingredientEditor.Text = string.Empty;
        }
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var ingredient = e.SelectedItem as Ingredient;
        if (ingredient != null)
        {
            ingredientEditor.Text = ingredient.Name;
        }
    }
}