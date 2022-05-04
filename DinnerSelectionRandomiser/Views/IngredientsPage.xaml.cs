using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser.Views
{
    public partial class IngredientsPage : ContentPage, INotifyPropertyChanged
    {
        public IngredientsPage(List<string> ShoppingList)
        {
            if (ShoppingList.Count == 0) return; //If the passed in list is empty, do nothing
            
            InitializeComponent();

            ShoppingList.Sort(); //Arrange the passed in list alphabetically
            BindingContext = ShoppingList;
        }
    }
}