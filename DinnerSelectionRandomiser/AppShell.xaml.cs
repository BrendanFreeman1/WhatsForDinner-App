using DinnerSelectionRandomiser.Views;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddDinnersPage), typeof(AddDinnersPage));
            Routing.RegisterRoute(nameof(IngredientsPage), typeof(IngredientsPage));
        }

    }
}
