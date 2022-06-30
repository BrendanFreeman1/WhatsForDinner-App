using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using DinnerSelectionRandomiser.Models;

namespace DinnerSelectionRandomiser.Data
{
    public class DinnersDatabase
    {
        readonly SQLiteAsyncConnection database;

        public DinnersDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Dinner>().Wait();
        }

        public Task<List<Dinner>> GetDinnersAsync()
        {
            //Get all Dinners.
            return database.Table<Dinner>().ToListAsync();
        }

        public Task<Dinner> GetDinnerAsync(int id)
        {
            // Get a specific Dinner.
            return database.Table<Dinner>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveDinnerAsync(Dinner dinner)
        {
            if (dinner.ID != 0)
            {
                // Update an existing Dinner.
                return database.UpdateAsync(dinner);
            }
            else
            {
                return database.InsertAsync(dinner);
            }
        }

        public Task<int> DeleteDinnerAsync(Dinner dinner)
        {
            // Delete a Dinner.
            return database.DeleteAsync(dinner);
        }
    }
}
