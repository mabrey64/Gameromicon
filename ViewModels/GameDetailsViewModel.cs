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
    public partial class GameDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Game? currentGame;

        [ObservableProperty]
        private string? searchTitleQuery;

        [ObservableProperty]
        private ObservableCollection<GameConsole> availablePlatforms;

        [ObservableProperty]
        private GameConsole? selectedSearchPlatform;

        [ObservableProperty]
        private ObservableCollection<string> availableConditions;

        [ObservableProperty]
        private ObservableCollection<Publisher> availablePublishers;

        [ObservableProperty]
        private Publisher? selectedPublisher;

        [ObservableProperty]
        private ObservableCollection<Series> availableSeries;

        [ObservableProperty]
        private Series? selectedSeries;

        public GameDetailsViewModel()
        {
            CurrentGame = new Game();
            availablePlatforms = new ObservableCollection<GameConsole>
            {
                new GameConsole {ID = 1, Name = "Nintendo Switch"},
                new GameConsole {ID = 2, Name = "PlayStation 5"},
                new GameConsole {ID = 3, Name = "Xbox Series X"},
                new GameConsole {ID = 4, Name = "PC"}
            };
            AvailableConditions = new ObservableCollection<string>
            {
                "New",
                "Like New",
                "Very Good",
                "Good",
                "Acceptable"
            };
            AvailablePublishers = new ObservableCollection<Publisher>
            {
                new Publisher { ID = 1, Name = "Nintendo" },
                new Publisher { ID = 2, Name = "Sony Interactive Entertainment" },
                new Publisher { ID = 3, Name = "Microsoft Studios" },
                new Publisher { ID = 4, Name = "Electronic Arts" },
                new Publisher { ID = 5, Name = "Sega" },
            };
            AvailableSeries = new ObservableCollection<Series>
            {
                new Series { ID = 1, Name = "The Legend of Zelda" },
                new Series { ID = 2, Name = "Final Fantasy" },
                new Series { ID = 3, Name = "Halo" },
                new Series { ID = 4, Name = "Call of Duty" },
                new Series { ID = 5, Name = "Super Mario" }
            };

        }
        [RelayCommand]
        public async Task ScanBarcode()
        {
            Debug.WriteLine("Scanning for a barcode...");
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task SearchExternalApiByTitle()
        {
            Debug.WriteLine($"Searching external API for '{SearchTitleQuery}' on platform '{selectedSearchPlatform?.Name}'...");
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task SaveGame()
        {
            if (CurrentGame != null && CurrentGame.Validate())
            {
                Debug.WriteLine($"Saving game: {CurrentGame.Name}");
            }
            else
            {
                Debug.WriteLine("Game validation failed. Unable to save game.");
            }
        }

        [RelayCommand]
        private async Task DeleteGame()
        {
            if (CurrentGame != null)
            {
                Debug.WriteLine($"Deleting game: {CurrentGame?.Name}");
                CurrentGame = null; // Clear the current game after deletion
            }
            else
            {
                Debug.WriteLine("No game to delete.");
            }
        }
    }
}
