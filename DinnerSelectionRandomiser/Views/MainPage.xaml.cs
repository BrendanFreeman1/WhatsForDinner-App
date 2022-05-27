using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinnerSelectionRandomiser.Models;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser.Views
{
    public partial class MainPage : ContentPage
    {
        readonly string thisWeeksDinners = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThisWeeksDinners.txt");

        List<Dinner> allDinnersList = new List<Dinner>();//List of all dinners taken from the apps database
        List<string> dinnersNames = new List<string>(); //List of the Names of the Dinners for the current week   
        readonly List<Dinner> dinners = new List<Dinner>(); //List of Dinners for current week          
        readonly Random rnd = new Random();
        string season = "";

        public MainPage()
        {
            InitializeComponent();
            PopulateDatabase();
            LoadSavedDinners();
            SetSeason();
        }

        protected override void OnAppearing()
        {
            PopulateDatabase(); //Reload Database when navigating back to this page
            SetBinding();
        }


        void PopulateDatabase()
        {
            //Get all Dinners from the database, wait until they're loaded before proceeding
            allDinnersList = Task.Run(async () => await App.Database.GetDinnerAsync()).Result;
        }

        void SetBinding()
        {            
            BindingContext = null;
            BindingContext = dinnersNames;
        }

        #region Randomise List
        void OnRandomiseClicked(object sender, EventArgs e)
        {
            PopulateDatabase(); //Make sure Database is up to date before drawing from it

            int dayOfTheWeek = 0;
            Dinner newDinner;
            dinners.Clear();
            dinnersNames.Clear();

            while (dayOfTheWeek < 7)
            {
                //As dinners are removed from the allDinnersList this will break out of loop when the list is empty
                if (allDinnersList.Count == 0) break; 

                //Get Random Dinner
                newDinner = GetRandomDinner(dayOfTheWeek);         
                
                //If the current dinner didn't pass the filter, start loop again
                if(newDinner == null) continue;

                //If the current dinner did pass the filter
                dinners.Add(newDinner);
                dinnersNames.Add(newDinner.Text);
                allDinnersList.Remove(newDinner);
                dayOfTheWeek++;
            }

            SetBinding();
        }

        Dinner GetRandomDinner(int dayOfTheWeek)
        {            
            int randomIndex;
            Dinner newDinner = null;

            while (newDinner == null)
            {
                //As dinners are removed from the allDinnersList this will break out of loop when the list is empty
                //CHECK IF LOGIC CAN BE REWORKED TO REMOVE CHECKS AT EACH STAGE OF FINDING DINNERS FOR THE WEEK!!?!
                if (allDinnersList.Count == 0) break;

                //Get a random number between 0 and the lenth of the allDinnersList
                randomIndex = rnd.Next(0, allDinnersList.Count);
                //Grab the dinner at the index of that random number
                newDinner = allDinnersList[randomIndex];

                //Pass this dinner through the filters to see if its suitable with the dinners we already have
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

            for (int i = 0; i < dinners.Count; i++)
            {
                //If the current Dinner is the same type as a previous dinner in the list, Get a new one
                if (newDinner.Type == dinners[i].Type)
                {
                    allDinnersList.Remove(newDinner);
                    return true;
                }
            }

            return false;
        }

        void RandomiseSingleDinner(int dayOfTheWeek)
        {
            //If the dinners list isn't fully populated, do nothing
            if (dinners.Count < 7) return;

            int randomIndex = rnd.Next(0, allDinnersList.Count);
            Dinner newDinner = allDinnersList[randomIndex];

            dinners[dayOfTheWeek] = newDinner;
            dinnersNames[dayOfTheWeek] = newDinner.Text;
            SetBinding();
        }
        #endregion

        #region Save To File
        void OnSaveClicked(object sender, EventArgs e)
        {
            //If theres nothing to save, do nothing
            if (dinners.Count == 0) return;
            
            //The String of this weeks Dinners to save to file
            string dinnersToSave = "";

            //Construct the string but adding the name of each dinner plus a comma.
            //Dont add a comma on the last entry
            for (int i = 0; i < dinners.Count; i++)
            {
                if (i < dinners.Count - 1) { dinnersToSave += dinners[i].Text + ","; }
                else { dinnersToSave += dinners[i].Text; } //Don't add the ',' to the last entry
            }

            //Write the Dinners to file
            File.WriteAllText(thisWeeksDinners, dinnersToSave);            

        }

        void LoadSavedDinners()
        {            
            //Check if the file exists
            if (File.Exists(thisWeeksDinners))
            {
                //Parse out each dinners name from the thisWeeksDinners string
                dinnersNames = File.ReadAllText(thisWeeksDinners).Split(',').ToList();

                foreach (string dinnerName in dinnersNames)
                {
                    foreach (Dinner dinner in allDinnersList)
                    {
                        if (dinner.Text == dinnerName)
                        {
                            dinners.Add(dinner);
                            allDinnersList.Remove(dinner);
                        }
                    }
                }          
            }
        }
        #endregion

        #region Ingredients
        void OnIngredientsClicked(object sender, EventArgs e)
        {
            if (dinners.Count == 0) return;
            
            //Navigate to the Ingredients Page, Passing through the 'shoppingList' List
            Navigation.PushAsync(new IngredientsPage(CreateIngredientsList()));            
        }

        List<string> CreateIngredientsList()
        {
            List<string> shoppingList = new List<string>();

            foreach (Dinner dinner in dinners)
            {
                List<string> thisDinnersIngredients = dinner.Ingredients.Split(',').ToList();

                foreach (string ingredient in thisDinnersIngredients)
                {
                    //Remove leading white space from string
                    StringBuilder sb = new StringBuilder(ingredient);
                    if (sb[0] == ' ') sb.Remove(0, 1);
                    
                    shoppingList.Add(sb.ToString());    
                }
            }

            return shoppingList.Distinct().ToList();
        }

        #endregion

        #region Season
        void SetSeason()
        {
            //Set the season
            if (DateTime.Today.Month <= 3 || DateTime.Today.Month >= 9) //Spring and Summer
            {
                season = "SUMMER";
                SeasonButton.Text = season;
            }
            else // Autumn and Winter
            {
                season = "WINTER";
                SeasonButton.Text = season;
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