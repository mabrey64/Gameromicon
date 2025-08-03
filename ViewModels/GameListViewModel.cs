using Gameromicon.Classes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
            GameCollection = new ObservableCollection<Game>()
            {
                new Game { ID = 1, Name = "The Legend of Zelda: Breath of the Wild", Barcode = "1234567890123", Boxart = "zelda_botw.jpg", CollectedDate = DateTime.Now, CiB = true, Condition = "New", Region = "NA", NumberOfCopies = 1, Description = "An open-world action-adventure game set in the kingdom of Hyrule.", ReleaseDate = new DateTime(2017, 3, 3), IsBeaten = false, Is100Completed = false, PublisherID = 1, SeriesID = 1, ProfileID = 1 },
                new Game { ID = 2, Name = "Super Mario Odyssey", Barcode = "1234567890124", Boxart = "mario_odyssey.jpg", CollectedDate = DateTime.Now, CiB = true, Condition = "Like New", Region = "EU", NumberOfCopies = 1, Description = "A platform game featuring Mario in a 3D world.", ReleaseDate = new DateTime(2017, 10, 27), IsBeaten = false, Is100Completed = false, PublisherID = 1, SeriesID = 2, ProfileID = 1 }

            };
        }

        [RelayCommand]
        private Task Sort()
        {
            Debug.WriteLine("Sorting games...");
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
                Debug.WriteLine("No game selected.");
                return;
            }
            Debug.WriteLine($"Selected game: {game.Name}");
            await Task.Delay(50);
            selectedGame = null;
        }
    }
}
