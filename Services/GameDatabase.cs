using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Gameromicon.Classes;
using System.Data.SqlTypes;
using System.Threading;

namespace Gameromicon.Services
{
    public class GameDatabase
    {
        private GameDatabase() { }

        private static GameDatabase _instance;
        public static GameDatabase Instance => _instance ??= new GameDatabase();

        private static SQLiteAsyncConnection _database;

        public async Task InitializeAsync()
        {
            if (_database != null)
            {
                return; // Database already initialized
            }
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gameromicon.db");
            _database = new SQLiteAsyncConnection(dbPath, SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache);

            // Create tables for each class
            await _database.CreateTableAsync<Game>();
            await _database.CreateTableAsync<Genre>();
            await _database.CreateTableAsync<Publisher>();
            await _database.CreateTableAsync<Series>();
            await _database.CreateTableAsync<GameConsole>();
            await _database.CreateTableAsync<Profile>();

            // Create junction tables for many-to-many relationships
            await _database.CreateTableAsync<GamePlatform>();
            await _database.CreateTableAsync<GameGenre>();
            await _database.CreateTableAsync<SeriesConsole>();
            await _database.CreateTableAsync<SeriesGenre>();
        }

        // Generic CRUD operations
        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : new()
        {
            return await _database.FindAsync<T>(id);
        }

        public async Task<int> SaveAsync<T>(T item) where T : new()
        {
                return await _database.InsertOrReplaceAsync(item);
        }

        public async Task<int> DeleteAsync<T>(T item) where T : new()
        {
            return await _database.DeleteAsync(item);
        }

        public async Task<List<GameConsole>> GetConsolesForGame(int gameId)
        {
            // Query the linking table (GamePlatform)
            var gamePlatforms = await _database.Table<GamePlatform>().Where(gp => gp.GameID == gameId).ToListAsync();
            var consoles = new List<GameConsole>();
            foreach (var gp in gamePlatforms)
            {
                // CORRECT: We use the ConsoleID from the GamePlatform linking table.
                var console = await GetByIdAsync<GameConsole>(gp.GameConsoleID);
                if (console != null)
                {
                    consoles.Add(console);
                }
            }
            return consoles;
        }

        public async Task<List<Genre>> GetGenresForGame(int gameId)
        {
            var gameGenres = await _database.Table<GameGenre>().Where(gg => gg.GameID == gameId).ToListAsync();
            var genres = new List<Genre>();
            foreach (var gameGenre in gameGenres)
            {
                var genre = await GetByIdAsync<Genre>(gameGenre.GenreID);
                if (genre != null)
                {
                    genres.Add(genre);
                }
            }
            return genres;
        }
    }
}
