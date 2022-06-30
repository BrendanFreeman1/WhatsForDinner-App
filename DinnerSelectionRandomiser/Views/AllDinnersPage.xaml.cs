using System;
using System.Linq;
using DinnerSelectionRandomiser.Models;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser.Views
{
    public partial class AllDinnersPage : ContentPage
    {
        public AllDinnersPage()
        {
            InitializeComponent();
        }

        //Populates the collection view with any data stored in the database
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetDinnersAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the AddDinnerPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(AddDinnersPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the AddDinnerPage, passing the ID as a query parameter.
                Dinner dinner = (Dinner)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(AddDinnersPage)}?{nameof(AddDinnersPage.ItemId)}={dinner.ID}");
            }
        }
    }
}