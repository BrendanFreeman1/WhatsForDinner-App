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
                new Dinner (0, "Pumpkin Soup", "Soup", "Pumpkin, Garlic, Onion, Cream, Vegetable Stock", "https://www.sbs.com.au/food/recipes/pumpkin-soup"),                
                new Dinner (0, "Tomato Soup",  "Soup", "Tin Tomatoes, Tomato Paste, Garlic, Onion, Potato, Vegetable Stock", "https://www.bbcgoodfood.com/recipes/tomato-soup" ),

                new Dinner (0, "Chicken Soup", "Soup", "Whole Chicken, Carrots, Celery, Onion, Garlic, Chicken Stock", "https://www.marthastewart.com/336138/basic-chicken-soup" ),
                new Dinner (0, "Leek and Potato Soup", "Soup", "Leek, Potato, Garlic, Butter, Vegetable Stock, Cream", "https://www.recipetineats.com/leek-and-potato-soup/" ),

                //Salad
                new Dinner (0, "Caesar Salad", "Salad", "Cos Lettuce, Chicken or Bacon, Croutons, Eggs, Cheese, Aioli", ""),
                new Dinner (0, "Halloumi Salad", "Salad", "Spinich, Halloumi, Capsicum, Cucumber", "" ),
                new Dinner (0, "Herbed Potato Salad", "Salad", "Potato, Shallots, Lemon Juice, Dijon Mustard, Celery, Parsley", "" ),

                //Italian
                new Dinner (0, "Bolognese", "Italian", "Mince, Spaghetti, Carrots, Celery, Onion, Garlic, Tin Tomatoes, Tomato Paste, Chicken Stock, Cheese", "https://www.sbs.com.au/food/recipes/classic-bolognese" ),
                new Dinner (0, "Vegetable Lasagne", "Italian", "Pasta Sheets, Pumpkin, Tin Tomatoes, Lentiles, Spinach, Bechamel, Cheese, Butter", "https://www.goodfood.com.au/recipes/vegetable-lasagne-20130725-2qlfc" ),
                new Dinner (0, "Mince Lasagne", "Italian", "Pasta Sheets, Mince, Onion, Garlic, Carrot, Celery, Tin Tomatoes, Tomato Paste, Chicken Stock, Bechamel, Cheese", "https://sanremo.com.au/recipes/easy-meat-lasagna/" ),
                new Dinner (0, "Cabonara", "Italian", "Bacon, Garlic, Fettuccine, Cheese, Cream, Eggs", "" ),
                new Dinner (0, "Pesto Pasta", "Italian", "Basil Pesto, Bacon, Garlic, Penne Pasta, Cheese, Cream", "" ),
                new Dinner (0, "Pasta Bake", "Italian", "Macaroni Pasta, Pasta Bake Sauce, Cheese, Pasta Bake", "" ),
                new Dinner (0, "Browned Butter Gnocchi", "Italian", "Gnocchi, Butter, Garlic", "" ),
                new Dinner (0, "Tomato Gnocchi", "Italian", "Gnocchi, Onion, Garlic, Tin Tomatoes, Italian Herbs", "" ),
                new Dinner (0, "Pesto Gnocchi", "Italian", "Gnocchi, Garlic, Basil Pesto, Cream", "" ),
                new Dinner (0, "Cream Gnocchi", "Italian", "Gnocchi, Bacon, Cream, Cheese", "" ),
                new Dinner (0, "Sweet Potato Gnocchi", "Italian", "Gnocchi, Parmesan, Milk, Sweet Potato, Spinach, Cinnamon, Chickpeas, Ricotta, Sage, Garlic", "https://www.woolworths.com.au/shop/recipes/gnocchi-with-sweet-potato-sauce" ),

                new Dinner (0, "Chicken Risotto", "Italian", "Chicken, Chicken Stock, Butter, Onion, Garlic, Arborio Rice", ""),
                new Dinner (0, "Tomato Risotto", "Italian", "Tin Tomatoes, Vegatable Stock, Butter, Onion, Garlic, Rosemary, Arborio Rice, Cherry Tomatoes, Basil, Parmesan", "https://www.bbcgoodfood.com/recipes/creamy-tomato-risotto" ),
                new Dinner (0, "Bacon Thyme Risotto", "Italian", "Bacon, Chicken Stock, Butter, Onion, Garlic, Thyme, Arborio Rice", "https://www.delish.com/cooking/recipe-ideas/a24280714/instant-pot-risotto/" ),

                //Asian
                new Dinner (0, "Honey Chicken", "Asian", "Chicken, Honey, Rice, Soy Sauce, Eggs, Lemon, Sesame Oil", "https://www.youtube.com/watch?vNqwW4BiLoBw" ),
                new Dinner (0, "Satay Chicken", "Asian", "Chicken, Peanut Butter, Rice, Onion, Garlic, Soy Sauce, Coconut Milk", "" ),
                new Dinner (0, "Chicken Stirfry", "Asian", "Chicken, Noodles, Onion, Beans, Capsicum, Soy Sauce, Sesame Oil, Shallots", "" ),

                //Burger
                new Dinner (0, "Beef Burger", "Burger", "Mince Meat, Burger Buns, Lettuce, Tomato, Red Onion, Cheese", "" ),
                new Dinner (0, "Chicken Burger", "Burger", "Chicken, Burger Buns, Lettuce, Tomato, Avocado, Red Onion, Cheese", "" ),
                new Dinner (0, "Turkey Burger", "Burger", "Turkey Mince, Pita Bread, Lemon, Dried Thyme, Beetroot, Red Onion, Mustard", "https://www.bbcgoodfood.com/recipes/turkey-burgers-beetroot-relish" ),

                //Chicken
                new Dinner (0, "Chicken Schnitzel", "Chicken", "Chicken, Bread Crumbs, Flour, Egg", "" ),
                new Dinner (0, "Chicken Parmigiana", "Chicken", "Chicken, Bread Crumbs, Flour, Egg, Tomato Pase, Cheese", "" ),
                new Dinner (0, "Chicken Nuggets", "Chicken", "Chicken, Bread Crumbs, Flour, Egg", "" ),
                new Dinner (0, "Chicken Wings", "Chicken", "Chicken Wings, Flour, Baking Soda, Herbs", "" ),
                new Dinner (0, "Grilled Chicken", "Chicken", "Chicken, Garlic, Herbs", "" ),
                new Dinner (0, "Chicken Spaghetti", "Chicken", "Chicken, Spaghettim, Onion, Tomato Paste, Garlic, Thyme, Parmesan, Break Crumbs, Lemon", "https://www.delicious.com.au/recipes/one-pan-crispy-spaghetti-chicken-recipe/a3k3q2xi" ),
                new Dinner (0, "Black Basil Chicken", "Chicken", "Chicken, Soy Sauce, Sesame Oil, Cornstarch, Basil leaves, Garlic, Shallots", "https://www.goodfood.com.au/recipes/black-basil-chicken-20200518-h1o51l" ),
                new Dinner (0, "Lemon Chicken and Herbs", "Chicken", "Chicken, Mustard, Lemon, Dried Oregano, Dried Thyme, Chicken Stock, Garlic, Butter, Flour", "https://www.inspiredtaste.net/18649/easy-lemon-chicken-recipe/#itr-recipe-18649" ),

                //Roast
                new Dinner (0, "Roast Chicken", "Weekend", "Whole Chicken, Roasting Vegetables", "" ),
                new Dinner (0, "Pulled Pork", "Weekend", "Pork Shoulder, Apple Sauce", "https://www.bodyweightwarrior.co.uk/blog/simple-pulled-pork" ),
                new Dinner (0, "Roast Lamb", "Weekend", "Lamb, Garlic, Rosemary, Roasting Vegetables", "" ),

                //Other
                new Dinner (0, "Pizza", "Weekend", "Flour, Yeast, Tomato Paste, Cheese, Feta, Deli Meat, Capsicum, Red Onion", "" ),
                new Dinner (0, "Gozleme", "Other", "Flour, Feta, Spinich, Garlic, Eggs, Cheese", "https://www.recipetineats.com/gozleme/" ),
                new Dinner (0, "Quiche", "Other", "Eggs, Bacon, Oats, Self Raising Flour, Butter, Cheese", "https://www.bestrecipes.com.au/recipes/easy-quiche-recipe/bym5vl9w" ),
                new Dinner (0, "Kangaroo Steak", "Redmeat", "Kangaroo Steaks, Potatoes", "" ),
                new Dinner (0, "Lamb Schnitzel", "Weekend", "Lamb Cutlets, Flour, Egg, Bread Crumbs", "https://www.australianlamb.com.au/recipes/parmesan-lamb-schnitzel/#" ),
                new Dinner (0, "Fritters", "Weekend", "Bacon, Onions, Garlic, Zucchini, Sweet Potato, Cheese, Self Raising Flour, Milk, Eggs", "https://www.taste.com.au/recipes/cheesy-bacon-zucchini-fritters/rzsovbc0?fbclidIwAR3m_2PwpcAonijBIYj4sYakiWn7BOqbpJBx5QEtrDDd5i6jUxjs02sCemk" ),
                new Dinner (0, "Vietnamese Pancakes", "Other", "Rice Flour, Flour, Coconut Cream, Shallots, Pork, Bean Sprouts, Mung Beans, Spinach", "https://www.hungryhuy.com/banh-xeo-savory-vietnamese-crepes/" ),
                new Dinner (0, "Greek Tray Bake", "Other", "Lamb Mince, Bread Crumbs, Feta, Potatoes, Zucchini, Cherry Tomatoes, Eggs, Onion", "https://www.bbcgoodfood.com/recipes/greek-lamb-tray-bake" ), 

                //Mexican
                new Dinner (0, "Tacos", "Mexican", "Mince, Taco Shells, Tin Tomatoes, Tomato Paste, Onion, Garlic, Carrot, Cheese", "" ),
                new Dinner (0, "Fajitas", "Mexican", "Chicken, Wraps, Capsicum, Onion, Garlic, Cheese", "" ),
                new Dinner (0, "Enchiladas", "Mexican", "Mince, Wraps, Tin Tomatoes, Tomato Paste, Capsicum, Onion, Garlic, Cheese", "" )
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
