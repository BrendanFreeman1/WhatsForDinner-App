using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser.Views
{
    public partial class IngredientsPage : ContentPage, INotifyPropertyChanged
    {
        public IngredientsPage(List<string> ShoppingList)
        {
            InitializeComponent();
            BindingContext = ShoppingList;
        }
    }
}