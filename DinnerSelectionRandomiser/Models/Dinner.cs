using System.Windows.Input;
using Xamarin.Essentials;
using SQLite;

namespace DinnerSelectionRandomiser.Models
{
    public class Dinner
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public string Type{ get; set; }
        public string Ingredients { get; set; }
        public string Recipe { get; set; }

        public Dinner(string text, string type, string ingredients, string recipe)
        {
            Text = text;
            Type = type;
            Ingredients = ingredients;
            Recipe = recipe;
        }

        public Dinner()
        {
            OpenWebCommand = new Xamarin.Forms.Command(async () => await Browser.OpenAsync(Recipe));
        }

        public ICommand OpenWebCommand { get; }
    }
}
