using Gameromicon.Classes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gameromicon.Pages;

namespace Gameromicon.ViewModels
{
    public partial class GameListViewModel : BaseViewModel
    {
        
        public string SearchQuery
        {
            get => searchQuery;
            set => SetProperty(ref searchQuery, value);
        }
        private string searchQuery = string.Empty;

        public ObservableCollection<Game> GameCollection
        {
            get => gameCollection;
            set => SetProperty(ref gameCollection, value);
        }
        private ObservableCollection<Game> gameCollection;

        public Game? SelectedGame
        { get => selectedGame; 
          set => SetProperty(ref selectedGame, value);
        }
        private Game? selectedGame;

        public GameListViewModel()
        {
            GameCollection = new ObservableCollection<Game>
    {
        new Game
        {
            ID = 1,
            Name = "The Legend of Zelda: Breath of the Wild",
            Barcode = "1234567890",
            Boxart = "",
            CollectedDate = DateTime.Now.AddMonths(-12),
            CiB = true,
            Condition = "Like New",
            Region = "NA",
            NumberOfCopies = 1,
            Description = "Open-world adventure game.",
            ReleaseDate = new DateTime(2017, 3, 3),
            IsBeaten = true,
            Is100Completed = false,
            PublisherID = 1,
            SeriesID = 1
        },
        new Game
        {
            ID = 2,
            Name = "Halo Infinite",
            Barcode = "0987654321",
            Boxart = "",
            CollectedDate = DateTime.Now.AddMonths(-6),
            CiB = false,
            Condition = "Good",
            Region = "NA",
            NumberOfCopies = 1,
            Description = "First-person shooter.",
            ReleaseDate = new DateTime(2021, 12, 8),
            IsBeaten = false,
            Is100Completed = false,
            PublisherID = 3,
            SeriesID = 3
        },
        new Game
        {
            ID = 3,
            Name = "Final Fantasy VII Remake",
            Barcode = "1122334455",
            Boxart = "",
            CollectedDate = DateTime.Now.AddMonths(-18),
            CiB = true,
            Condition = "Very Good",
            Region = "JP",
            NumberOfCopies = 2,
            Description = "Remake of the classic RPG.",
            ReleaseDate = new DateTime(2020, 4, 10),
            IsBeaten = true,
            Is100Completed = true,
            PublisherID = 2,
            SeriesID = 2
        }
    };
        }

        [RelayCommand]
        private Task Sort()
        {
            var sorted = GameCollection.OrderBy(g => g.Name).ToList();
            GameCollection = new ObservableCollection<Game>(sorted);
            Debug.WriteLine("Games sorted by name.");
            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task Filter()
        {
            Debug.WriteLine("Filtering games...");

            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task AddGame()
        {
            Debug.WriteLine("Adding a new game...");
            // Logic to add a new game
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task SelectGame(Game game)
        {
            if (game == null)
            {
                await Application.Current.MainPage.DisplayAlert("No Selection", "Please select a game.", "OK");
                return;
            }

            // Navigate to AddGamePage, passing the selected game for editing
            await Application.Current.MainPage.Navigation.PushAsync(new AddGamePage(this, game));
        }

    }
}
