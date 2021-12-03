using System;
using System.IO;
using System.Collections.Generic;
using DinnerSelectionRandomiser.Data;
using DinnerSelectionRandomiser.Models;
using Xamarin.Forms;

namespace DinnerSelectionRandomiser
{
    public partial class App : Application
    {
        static DinnersDatabase database;

        // Create the database connection as a singleton.
        public static DinnersDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DinnersDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();          

            MainPage = new AppShell();
        }

        async protected override void OnStart()
        {
            List<Dinner> DatabaseList = await Database.GetDinnerAsync();
            List<Dinner> AllDinnersList = new List<Dinner>
            {
                //Soup
                new Dinner { ID = 0, Text = "Pumpkin Soup", Type = "Soup", Ingredients = "Pumpkin, Garlic, Onion, Cream, Vegetable Stock", Recipe = "https://www.sbs.com.au/food/recipes/pumpkin-soup" },
                new Dinner { ID = 0, Text = "Tomato Soup", Type = "Soup", Ingredients = "Tin Tomatoes, Tomato Paste, Garlic, Onion, Potato, Vegetable Stock", Recipe = "https://www.bbcgoodfood.com/recipes/tomato-soup" },
                new Dinner { ID = 0, Text = "Chicken Soup", Type = "Soup", Ingredients = "Whole Chicken, Carrots, Celery, Onion, Garlic, Chicken Stock", Recipe = "https://www.marthastewart.com/336138/basic-chicken-soup" },
                new Dinner { ID = 0, Text = "Leek and Potato Soup", Type = "Soup", Ingredients = "Leek, Potato, Garlic, Butter, Vegetable Stock, Cream", Recipe = "https://www.recipetineats.com/leek-and-potato-soup/" },

                //Salad
                new Dinner { ID = 0, Text = "Caesar Salad", Type = "Salad", Ingredients = "Cos Lettuce, Chicken or Bacon, Croutons, Eggs, Cheese, Aioli" },
                new Dinner { ID = 0, Text = "Halloumi Salad", Type = "Salad", Ingredients = "Spinich, Halloumi, Capsicum, Cucumber" },
                new Dinner { ID = 0, Text = "Herbed Potato Salad", Type = "Salad", Ingredients = "Potato, Shallots, Lemon Juice, Dijon Mustard, Celery, Parsley" },

                //Italian
                new Dinner { ID = 0, Text = "Bolognese", Type = "Italian", Ingredients = "Mince, Spaghetti, Carrots, Celery, Onion, Garlic, Tin Tomatoes, Tomato Paste, Chicken Stock, Cheese", Recipe = "https://www.sbs.com.au/food/recipes/classic-bolognese" },
                new Dinner { ID = 0, Text = "Vegetable Lasagne", Type = "Italian", Ingredients = "Pasta Sheets, Pumpkin, Tin Tomatoes, Lentiles, Spinich, Bechamel, Cheese, Butter", Recipe = "https://www.goodfood.com.au/recipes/vegetable-lasagne-20130725-2qlfc" },
                new Dinner { ID = 0, Text = "Mince Lasagne", Type = "Italian", Ingredients = "Pasta Sheets, Mince, Onion, Garlic, Carrot, Celery, Tin Tomatoes, Tomato Paste, Chicken Stock, Bechamel, Cheese", Recipe = "https://sanremo.com.au/recipes/easy-meat-lasagna/" },
                new Dinner { ID = 0, Text = "Cabonara", Type = "Italian", Ingredients = "Bacon, Garlic, Fettuccine, Cheese, Cream, Eggs" },
                new Dinner { ID = 0, Text = "Pesto Pasta", Type = "Italian", Ingredients = "Basil Pesto, Bacon, Garlic, Penne Pasta, Cheese, Cream" },
                new Dinner { ID = 0, Text = "Pasta Bake", Type = "Italian", Ingredients = "Macaroni Pasta, Pasta Bake Sauce, Cheese, Pasta Bake Ingredients" },
                new Dinner { ID = 0, Text = "Browned Butter Gnocchi", Type = "Italian", Ingredients = "Gnocchi, Butter, Garlic" },
                new Dinner { ID = 0, Text = "Tomato Gnocchi", Type = "Italian", Ingredients = "Gnocchi, Onion, Garlic, Tin Tomatoes, Italian Herbs" },
                new Dinner { ID = 0, Text = "Pesto Gnocchi", Type = "Italian", Ingredients = "Gnocchi, Garlic, Basil Pesto, Cream" },
                new Dinner { ID = 0, Text = "Cream Gnocchi", Type = "Italian", Ingredients = "Gnocchi, Bacon, Cream, Cheese" },
                new Dinner { ID = 0, Text = "Sweet Potato Gnocchi", Type = "Italian", Ingredients = "Gnocchi, Parmesan, Milk, Sweet Potato, Spinach, Cinnamon, Chickpeas, Ricotta, Sage, Garlic", Recipe = "https://www.woolworths.com.au/shop/recipes/gnocchi-with-sweet-potato-sauce" },

                new Dinner { ID = 0, Text = "Chicken Risotto", Type = "Italian", Ingredients = "Chicken, Chicken Stock, Butter, Onion, Garlic, Arborio Rice", Recipe = "" },
                new Dinner { ID = 0, Text = "Tomato Risotto", Type = "Italian", Ingredients = "Tin Tomatoes, Vegatable Stock, Butter, Onion, Garlic, Rosemary, Arborio Rice, Cherry Tomatoes, Basil, Parmesan", Recipe = "https://www.bbcgoodfood.com/recipes/creamy-tomato-risotto" },
                new Dinner { ID = 0, Text = "Bacon Thyme Risotto", Type = "Italian", Ingredients = "Bacon, Chicken Stock, Butter, Onion, Garlic, Thyme, Arborio Rice", Recipe = "https://www.delish.com/cooking/recipe-ideas/a24280714/instant-pot-risotto/" },

                //Asian
                new Dinner { ID = 0, Text = "Honey Chicken", Type = "Asian", Ingredients = "Chicken, Honey, Rice, Soy Sauce, Eggs, Lemon, Sesame Oil", Recipe = "https://www.youtube.com/watch?v=NqwW4BiLoBw" },
                new Dinner { ID = 0, Text = "Satay Chicken", Type = "Asian", Ingredients = "Chicken, Peanut Butter, Rice, Onion, Garlic, Soy Sauce, Coconut Milk" },
                new Dinner { ID = 0, Text = "Chicken Stirfry", Type = "Asian", Ingredients = "Chicken, Noodles, Onion, Beans, Capsicum, Soy Sauce, Sesame Oil, Shallots" },

                //Burger
                new Dinner { ID = 0, Text = "Beef Burger", Type = "Burger", Ingredients = "Mince Meat, Burger Buns, Lettuce, Tomato, Red Onion, Cheese" },
                new Dinner { ID = 0, Text = "Chicken Burger", Type = "Burger", Ingredients = "Chicken, Burger Buns, Lettuce, Tomato, Avocado, Red Onion, Cheese" },
                new Dinner { ID = 0, Text = "Turkey Burger", Type = "Burger", Ingredients = "Turkey Mince, Pita Bread, Lemon, Dried Thyme, Beetroot, Red Onion, Mustard", Recipe = "https://www.bbcgoodfood.com/recipes/turkey-burgers-beetroot-relish" },

                //Chicken
                new Dinner { ID = 0, Text = "Chicken Schnitzel", Type = "Chicken", Ingredients = "Chicken, Bread Crumbs, Flour, Egg" },
                new Dinner { ID = 0, Text = "Chicken Parmigiana", Type = "Chicken", Ingredients = "Chicken, Bread Crumbs, Flour, Egg, Tomato Pase, Cheese" },
                new Dinner { ID = 0, Text = "Chicken Nuggets", Type = "Chicken", Ingredients = "Chicken, Bread Crumbs, Flour, Egg" },
                new Dinner { ID = 0, Text = "Chicken Wings", Type = "Chicken", Ingredients = "Chicken Wings, Flour, Baking Soda, Herbs" },
                new Dinner { ID = 0, Text = "Grilled Chicken", Type = "Chicken", Ingredients = "Chicken, Garlic, Herbs" },
                new Dinner { ID = 0, Text = "Chicken Spaghetti", Type = "Chicken", Ingredients = "Chicken, Spaghettim, Onion, Tomato Paste, Garlic, Thyme, Parmesan, Break Crumbs, Lemon", Recipe = "https://www.delicious.com.au/recipes/one-pan-crispy-spaghetti-chicken-recipe/a3k3q2xi" },
                new Dinner { ID = 0, Text = "Black Basil Chicken", Type = "Chicken", Ingredients = "Chicken, Soy Sauce, Sesame Oil, Cornstarch, Basil leaves, Garlic, Shallots", Recipe = "https://www.goodfood.com.au/recipes/black-basil-chicken-20200518-h1o51l" },
                new Dinner { ID = 0, Text = "Lemon Chicken and Herbs", Type = "Chicken", Ingredients = "Chicken, Mustard, Lemon Zest + Juice, Dried Oregano, Dried Thyme, Chicken Stock, Garlic, Butter, Flour", Recipe = "https://www.inspiredtaste.net/18649/easy-lemon-chicken-recipe/#itr-recipe-18649" },

                //Roast
                new Dinner { ID = 0, Text = "Roast Chicken", Type = "Weekend", Ingredients = "Whole Chicken, Roasting Vegetables" },
                new Dinner { ID = 0, Text = "Pulled Pork", Type = "Weekend", Ingredients = "Pork Shoulder, Apple Sauce", Recipe = "https://www.bodyweightwarrior.co.uk/blog/simple-pulled-pork" },
                new Dinner { ID = 0, Text = "Roast Lamb", Type = "Weekend", Ingredients = "Lamb, Garlic, Rosemary, Roasting Vegetables" },
                
                //Other
                new Dinner { ID = 0, Text = "Pizza", Type = "Weekend", Ingredients = "Flour, Yeast, Tomato Paste, Cheese, Feta, Deli Meat, Capsicum, Red Onion" },
                new Dinner { ID = 0, Text = "Gozleme", Type = "Other", Ingredients = "Flour, Feta, Spinich, Garlic, Eggs, Cheese", Recipe = "https://www.recipetineats.com/gozleme/" },
                new Dinner { ID = 0, Text = "Quiche", Type = "Other", Ingredients = "Eggs, Bacon, Oats, Self Raising Flour, Butter, Cheese", Recipe = "https://www.bestrecipes.com.au/recipes/easy-quiche-recipe/bym5vl9w" },
                new Dinner { ID = 0, Text = "Kangaroo Steak", Type = "Redmeat", Ingredients = "Kangaroo Steaks, Potatoes" },
                new Dinner { ID = 0, Text = "Lamb Schnitzel", Type = "Weekend", Ingredients = "Lamb Cutlets, Flour, Egg, Bread Crumbs", Recipe = "https://www.australianlamb.com.au/recipes/parmesan-lamb-schnitzel/#" },
                new Dinner { ID = 0, Text = "Fritters", Type = "Weekend", Ingredients = "Bacon, Onions, Garlic, Zucchini, Sweet Potato, Cheese, Self Raising Flour, Milk, Eggs", Recipe = "https://www.taste.com.au/recipes/cheesy-bacon-zucchini-fritters/rzsovbc0?fbclid=IwAR3m_2PwpcAonijBIYj4sYakiWn7BOqbpJBx5QEtrDDd5i6jUxjs02sCemk" },
                new Dinner { ID = 0, Text = "Calamari and Chips", Type = "Other", Ingredients = "Frozen Calamari, Frozen Chips" },
                new Dinner { ID = 0, Text = "Vietnamese Pancakes", Type = "Other", Ingredients = "Rice Flour, Flour, Coconut Cream, Shallots, Pork, Bean Sprouts, Mung Beans, Spinach", Recipe = "https://www.hungryhuy.com/banh-xeo-savory-vietnamese-crepes/" },
                new Dinner { ID = 0, Text = "Greek Tray Bake", Type = "Other", Ingredients = "Lamb Mince, Bread Crumbs, Feta, Potatoes, Zucchini, Cherry Tomatoes, Eggs, Onion", Recipe = "https://www.bbcgoodfood.com/recipes/greek-lamb-tray-bake" },     

                //Mexican
                new Dinner { ID = 0, Text = "Tacos", Type = "Mexican", Ingredients = "Mince, Taco Shells, Tin Tomatoes, Tomato Paste, Onion, Garlic, Carrot, Cheese" },
                new Dinner { ID = 0, Text = "Fajitas", Type = "Mexican", Ingredients = "Chicken, Wraps, Capsicum, Onion, Garlic, Cheese" },
                new Dinner { ID = 0, Text = "Enchiladas", Type = "Mexican", Ingredients = "Mince, Wraps, Tin Tomatoes, Tomato Paste, Capsicum, Onion, Garlic, Cheese" }
        };

            
            foreach (Dinner dinner in AllDinnersList)
            {
                //If this dinner is already in the database, skip it.
                if (!IsAlreadyInDatabase(DatabaseList, dinner))
                {
                    await database.SaveDinnerAsync(dinner);
                }
            } 
        }

        bool IsAlreadyInDatabase(List<Dinner> DatabaseList, Dinner dinner)
        {
            foreach (Dinner d in DatabaseList)
            {
                if (d.Text == dinner.Text)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


    }
}
