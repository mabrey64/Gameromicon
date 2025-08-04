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
        private ObservableCollection<Classes.Platform> availableConsoles;

        [ObservableProperty]
        private string? selectedSearchConsole;

        [ObservableProperty]
        private ObservableCollection<string> availableConditions;

        [ObservableProperty]
        private ObservableCollection<string> availablePublishers;

        [ObservableProperty]
        private string? selectedPublisher;

        [ObservableProperty]
        private ObservableCollection<string> availableSeries;

        [ObservableProperty]
        private string? selectedSeries;

        public GameDetailsViewModel()
        {
            CurrentGame = new Game();
            AvailableConsoles = new ObservableCollection<string>
            {
                new Classes.Platform {ID = 1, Name = "Nintendo Switch"}, 
            };
            AvailableConditions = new ObservableCollection<string>
            {
                "New",
                "Like New",
                "Very Good",
                "Good",
                "Acceptable"
            };
            AvailablePublishers = new ObservableCollection<string>
            {
                "Nintendo",
                "Sony",
                "Microsoft",
                "Sega",
                "Atari",
                "Capcom",
                "Konami",
                "Electronic Arts"
            };
            AvailableSeries = new ObservableCollection<string>
            {
                "Super Mario",
                "The Legend of Zelda",
                "Final Fantasy",
                "Call of Duty",
                "Halo",
                "Pokemon",
                "Sonic the Hedgehog"
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
            Debug.WriteLine($"Searching external API for '{SearchTitleQuery}' on platform '{SelectedSearchConsole}'...");
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
