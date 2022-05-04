using System;
using DinnerSelectionRandomiser.Models;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AddDinnersPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadDinner(value);
            }
        }        

        public AddDinnersPage()
        {
            InitializeComponent();

            BindingContext = new Dinner();

            //Theres no webpage to navigate to so disable the button
            GoToRecipeButton.IsEnabled = false;
        }

        async void LoadDinner(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);

                // Retrieve the Dinner and set it as the BindingContext of the page.
                Dinner dinner = await App.Database.GetDinnerAsync(id);
                if(dinner.Recipe == null) { dinner.Recipe = ""; }

                BindingContext = dinner;

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load Dinner.");
            }

            //If theres no webpage to navigate to, disable the button
            if (RecipeEditor.Text == "") { GoToRecipeButton.IsEnabled = false; }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var dinner = (Dinner)BindingContext;
            if (!string.IsNullOrWhiteSpace(dinner.Text))
            {
                await App.Database.SaveDinnerAsync(dinner);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var dinner = (Dinner)BindingContext;
            await App.Database.DeleteDinnerAsync(dinner);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}