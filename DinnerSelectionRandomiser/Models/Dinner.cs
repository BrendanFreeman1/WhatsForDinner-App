using System.Windows.Input;
using Xamarin.Essentials;
using SQLite;
using System.Collections.Generic;
using System;

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
        
        public Dinner()
        {            
            OpenWebCommand = new Xamarin.Forms.Command(async () => await Browser.OpenAsync(Recipe));
        }

        public ICommand OpenWebCommand { get; }
    }
}
