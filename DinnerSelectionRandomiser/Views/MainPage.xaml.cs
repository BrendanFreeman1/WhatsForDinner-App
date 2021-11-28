using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DinnerSelectionRandomiser.Models;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser.Views
{
    public partial class MainPage : ContentPage
    {
        readonly string thisWeeksDinners = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThisWeeksDinners.txt");

        List<Dinner> allDinnersList = new List<Dinner>();//Database of all dinners in app
        readonly List<Dinner> dinners = new List<Dinner>(); //List of Dinners for current week
        readonly List<string> dinnersNames = new List<string>(); //List of the Names of the Dinners for the current week     
        readonly Random rnd = new Random();
        string season = "";

        public MainPage()
        {
            InitializeComponent();
            SetSeason();

            // Read the file.
            if (File.Exists(thisWeeksDinners))
            {
                //Parse out each dinner from the ThisWeeksDinners string
                dinnersNames = File.ReadAllText(thisWeeksDinners).Split(',').ToList();
            }
        }

        protected override void OnAppearing()
        {
            SetBinding();
            PopulateDatabase();
        }

        async protected void PopulateDatabase()
        {
            allDinnersList = await App.Database.GetDinnerAsync();
        }

        void SetBinding()
        {
            BindingContext = null;
            BindingContext = dinnersNames;
        }

        #region Randomise List
        void OnRandomiseClicked(object sender, EventArgs e)
        {
            PopulateDatabase();            

            int dayOfTheWeek = 0;
            Dinner newDinner;

            dinners.Clear();
            dinnersNames.Clear();

            while (dayOfTheWeek < 7)
            {
                if (allDinnersList.Count == 0) break;

                newDinner = GetRandomDinner(dayOfTheWeek);                
                if(newDinner == null) continue;

                dinners.Add(newDinner);
                dinnersNames.Add(newDinner.Text);
                dayOfTheWeek++;
            }

            SetBinding();
        }

        //Is Driving allDinnersList.Count to 0 
        void RandomiseSingleDinner(int dayOfTheWeek)
        {
            if (dinners.Count < 7) return;

            Dinner newDinner = GetRandomDinner(dayOfTheWeek);
            if (newDinner == null) return;

            dinners[dayOfTheWeek] = newDinner;
            dinnersNames[dayOfTheWeek] = newDinner.Text;
            SetBinding();
        }

        Dinner GetRandomDinner(int dayOfTheWeek)
        {            
            int randomIndex = 0;
            Dinner newDinner = null;

            while (newDinner == null)
            {
                if (allDinnersList.Count == 0) break;

                randomIndex = rnd.Next(0, allDinnersList.Count);
                newDinner = allDinnersList[randomIndex];

                if (Filter(newDinner, dayOfTheWeek)) newDinner = null;
            }

            return newDinner;
        }

        bool Filter(Dinner newDinner, int dayOfTheWeek)
        {
            //If the current dinner is a weekend meal but its not the weekend, Get a new one
            if (newDinner.Type == "Weekend" && dayOfTheWeek > 1)
            {
                allDinnersList.Remove(newDinner);
                return true;
            }
            //If its Winter and the current Dinner is a Salad dinner or If its Summer and the current Dinner is a Soup Dinner, Get a new one 
            if ((newDinner.Type == "Salad" && season == "WINTER") || (newDinner.Type == "Soup" && season == "SUMMER"))
            {
                allDinnersList.Remove(newDinner);
                return true;
            }

            //If the current Dinner has the same Main ingredient as the previous Dinner, Get a new one
            //Dont want to remove from the list!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (dayOfTheWeek > 0)
            {
                if (newDinner.MainIngredient == dinners[dayOfTheWeek - 1].MainIngredient)
                {
                    allDinnersList.Remove(newDinner);
                    return true; ;
                }
            }

            for (int i = 0; i < dinners.Count; i++)
            {
                //If the current Dinner is a repeat, Get a new one
                if (newDinner.Text == dinners[i].Text)
                {
                    allDinnersList.Remove(newDinner);
                    return true;
                }

                //If the current Dinner is the same type as a previous dinner in the list, Get a new one
                if (newDinner.Type == dinners[i].Type)
                {
                    allDinnersList.Remove(newDinner);
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Save To File
        void OnSaveClicked(object sender, EventArgs e)
        {
            if (dinners.Count == 0) return;

            //The String of this weeks Dinners to save to file
            string dinnersToSave = "";

            //Construct the string
            for (int i = 0; i < dinners.Count; i++)
            {
                if (i < dinners.Count - 1) { dinnersToSave += dinners[i].Text + ","; }
                else { dinnersToSave += dinners[i].Text; } //Don't add the ',' to the last entry
            }

            //Write the Dinners to file
            File.WriteAllText(thisWeeksDinners, dinnersToSave);
        }
        #endregion

        #region Ingredients
        void OnIngredientsClicked(object sender, EventArgs e)
        {
            //Ingredients arent saved to file. A smart system where a list or string is constructed for the weeks ingredients would condence the ingredients listing and allow for saving to file

            List<string> dinnersIngredients = new List<string>();

            foreach (Dinner dinner in dinners)
            {
                dinnersIngredients.Add(dinner.Ingredients);
            }

            //Navigate to the IngredientsPage, Passing through the 'DinnersIngredients' List
            Navigation.PushAsync(new IngredientsPage(dinnersIngredients));
        }
        #endregion

        #region Season
        void SetSeason()
        {
            //Set the IsWinter Bool
            if (DateTime.Today.Month <= 2 || DateTime.Today.Month >= 9) //Spring and Summer
            {
                season = "SUMMER";
                SeasonButton.Text = season;
            }
            else // Autumn and Winter
            {
                season = "WINTER";
                SeasonButton.Text = "Season";
            }
        }

        void OnSeasonClicked(object sender, EventArgs e)
        {
            //Swap the season
            if (season == "WINTER") season = "SUMMER";
            else season = "WINTER";

            //Set the Season button text
            SeasonButton.Text = season;
        }
        #endregion

        #region Randomise Single Day Methods
        void OnSatClicked(object sender, EventArgs e)
        {
            RandomiseSingleDinner(0);
        }
        void OnSunClicked(object sender, EventArgs e)
        {
            RandomiseSingleDinner(1);
        }
        void OnMonClicked(object sender, EventArgs e)
        {
            RandomiseSingleDinner(2);
        }
        void OnTueClicked(object sender, EventArgs e)
        {
            RandomiseSingleDinner(3);
        }
        void OnWedClicked(object sender, EventArgs e)
        {
            RandomiseSingleDinner(4);
        }
        void OnThurClicked(object sender, EventArgs e)
        {
            RandomiseSingleDinner(5);
        }
        void OnFriClicked(object sender, EventArgs e)
        {
            RandomiseSingleDinner(6);
        }
        #endregion
    }
}