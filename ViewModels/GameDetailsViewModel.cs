using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameromicon.Classes;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Gameromicon.ViewModels
{
    /// <summary>
    /// ViewModel for adding and editing game details.
    /// Handles all data binding and commands for the Add/Edit Game page.
    /// </summary>
    public partial class GameDetailsViewModel : BaseViewModel
    {
        // The game currently being added or edited
        [ObservableProperty]
        private Game? currentGame;

        // Search query for external API title search
        [ObservableProperty]
        private string? searchTitleQuery;

        // List of available platforms for selection
        [ObservableProperty]
        private ObservableCollection<GameConsole> availablePlatforms;

        // List of available conditions for selection
        [ObservableProperty]
        private ObservableCollection<string> availableConditions;

        // List of available publishers for selection
        [ObservableProperty]
        private ObservableCollection<Publisher> availablePublishers;

        // The publisher currently selected
        [ObservableProperty]
        private Publisher? selectedPublisher;

        // List of available series for selection
        [ObservableProperty]
        private ObservableCollection<Series> availableSeries;

        // The series currently selected
        [ObservableProperty]
        private Series? selectedSeries;

        // List of available genres for selection
        [ObservableProperty]
        private ObservableCollection<Genre> availableGenres;

        // The genres currently selected (multi-select)
        [ObservableProperty]
        private IList<Genre> selectedGenres = new List<Genre>();

        // The platform selected for searching external APIs
        [ObservableProperty]
        private GameConsole? selectedSearchPlatform;

        // The platform currently selected for the game
        [ObservableProperty]
        private GameConsole? selectedPlatform;

        /// <summary>
        /// Constructor initializes default values and populates reference data.
        /// </summary>
        public GameDetailsViewModel()
        {
            // Initialize a new game object for data binding
            CurrentGame = new Game();

            // Populate available platforms
            AvailablePlatforms = new ObservableCollection<GameConsole>
            {
                new GameConsole {ID = 1, Name = "Nintendo Switch"},
                new GameConsole {ID = 2, Name = "PlayStation 5"},
                new GameConsole {ID = 3, Name = "Xbox Series X"},
                new GameConsole {ID = 4, Name = "PC"}
            };

            // Populate available conditions
            AvailableConditions = new ObservableCollection<string>
            {
                "New",
                "Like New",
                "Very Good",
                "Good",
                "Acceptable"
            };

            // Populate available publishers
            AvailablePublishers = new ObservableCollection<Publisher>
            {
                new Publisher { ID = 1, Name = "Nintendo" },
                new Publisher { ID = 2, Name = "Sony Interactive Entertainment" },
                new Publisher { ID = 3, Name = "Microsoft Studios" },
                new Publisher { ID = 4, Name = "Electronic Arts" },
                new Publisher { ID = 5, Name = "Sega" },
            };

            // Populate available series
            AvailableSeries = new ObservableCollection<Series>
            {
                new Series { ID = 1, Name = "The Legend of Zelda" },
                new Series { ID = 2, Name = "Final Fantasy" },
                new Series { ID = 3, Name = "Halo" },
                new Series { ID = 4, Name = "Call of Duty" },
                new Series { ID = 5, Name = "Super Mario" }
            };

            // Populate available genres
            AvailableGenres = new ObservableCollection<Genre>
            {
                new Genre { ID = 1, Name = "Action" },
                new Genre { ID = 2, Name = "Adventure" },
                new Genre { ID = 3, Name = "Role-Playing" },
                new Genre { ID = 4, Name = "Simulation" },
                new Genre { ID = 5, Name = "Strategy" }
            };
        }

        /// <summary>
        /// Command to scan a barcode (stub for future implementation).
        /// </summary>
        [RelayCommand]
        public async Task ScanBarcode()
        {
            Debug.WriteLine("Scanning for a barcode...");
            await Task.CompletedTask;
        }

        /// <summary>
        /// Command to search an external API by title (stub for future implementation).
        /// </summary>
        [RelayCommand]
        private async Task SearchExternalApiByTitle()
        {
            Debug.WriteLine($"Searching external API for '{SearchTitleQuery}' on platform '{selectedSearchPlatform?.Name}'...");
            await Task.CompletedTask;
        }

        /// <summary>
        /// Command to save the current game (stub for future implementation).
        /// </summary>
        [RelayCommand]
        private async Task SaveGame()
        {
            if (CurrentGame != null && CurrentGame.Validate())
            {
                Debug.WriteLine($"Saving game: {CurrentGame.Name}");
                // Actual save logic should be implemented here
            }
            else
            {
                Debug.WriteLine("Game validation failed. Unable to save game.");
            }
        }

        /// <summary>
        /// Command to delete the current game (stub for future implementation).
        /// </summary>
        [RelayCommand]
        private async Task DeleteGame()
        {
            if (CurrentGame != null)
            {
                Debug.WriteLine($"Deleting game: {CurrentGame?.Name}");
                // Actual delete logic should be implemented here
                CurrentGame = null; // Clear the current game after deletion
            }
            else
            {
                Debug.WriteLine("No game to delete.");
            }
        }

        /// <summary>
        /// Command to fetch cover art for the current game from an external API.
        /// </summary>
        [RelayCommand]
        public async Task FetchCoverArt()
        {
            // Only attempt fetch if the game and its name are set
            if (CurrentGame != null && !string.IsNullOrWhiteSpace(CurrentGame.Name))
            {
                try
                {
                    using var http = new HttpClient();
                    // Replace with your actual API endpoint and parameters
                    var apiUrl = $"https://your-api.com/coverart?title={Uri.EscapeDataString(CurrentGame.Name)}";
                    var response = await http.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Assume the API returns a JSON object with a "coverUrl" property
                        var json = await response.Content.ReadAsStringAsync();
                        var coverUrl = System.Text.Json.JsonDocument.Parse(json).RootElement.GetProperty("coverUrl").GetString();
                        CurrentGame.CoverArtUrl = coverUrl;
                        OnPropertyChanged(nameof(CurrentGame));
                    }
                    else
                    {
                        // Optionally handle not found or error
                        CurrentGame.CoverArtUrl = null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error fetching cover art: {ex.Message}");
                    CurrentGame.CoverArtUrl = null;
                }
            }
        }
    }
}
