using Gameromicon.Classes;
using Gameromicon.ViewModels;
using System.Collections.ObjectModel;

namespace Gameromicon.Pages;

public partial class AddGamePage : ContentPage
{
	private readonly GameListViewModel _gameListViewModel;
    public static ObservableCollection<GameGenre> GameGenreCollection { get; } = new();
    public static ObservableCollection<GamePlatform> GamePlatformCollection { get; } = new();

    public AddGamePage(GameListViewModel gameListViewModel, Game? gameToEdit = null)
	{
		InitializeComponent();
		_gameListViewModel = gameListViewModel;
        var vm = new GameDetailsViewModel();
        if (gameToEdit != null)
        {
            // Clone or assign the game for editing
            vm.CurrentGame = gameToEdit;
            // Optionally set other properties (SelectedPublisher, etc.) based on gameToEdit
        }
        BindingContext = vm;
    }

    private async void OnAddGenreClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as GameDetailsViewModel;
        string result = await DisplayPromptAsync("Add Genre", "Enter the new genre name:");
        if (!string.IsNullOrWhiteSpace(result) && vm != null)
        {
            var newGenre = new Genre { ID = GenerateGenreId(vm), Name = result };
            vm.AvailableGenres.Add(newGenre);
            // Optionally, select the new genre by default
            vm.SelectedGenres.Add(newGenre);
        }
    }

    private int GenerateGenreId(GameDetailsViewModel vm)
    {
        return vm.AvailableGenres.Any() ? vm.AvailableGenres.Max(g => g.ID) + 1 : 1;
    }

    private async void OnAddPublisherClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as GameDetailsViewModel;
        string result = await DisplayPromptAsync("Add Publisher", "Enter the new publisher name:");
        if (!string.IsNullOrWhiteSpace(result) && vm != null)
        {
            var newPublisher = new Publisher { ID = GeneratePublisherId(vm), Name = result };
            vm.AvailablePublishers.Add(newPublisher);
            vm.SelectedPublisher = newPublisher;
        }
    }

    private int GeneratePublisherId(GameDetailsViewModel vm)
    {
        return vm.AvailablePublishers.Any() ? vm.AvailablePublishers.Max(p => p.ID) + 1 : 1;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as GameDetailsViewModel;
        var game = vm?.CurrentGame;
        if (game != null)
        {
            game.PublisherID = vm.SelectedPublisher?.ID ?? 0;
            game.SeriesID = vm.SelectedSeries?.ID;

            var genreIds = vm.SelectedGenres.Select(g => g.ID).ToList();
            var platformIds = vm.SelectedPlatform != null
                ? new List<int> { vm.SelectedPlatform.ID }
                : new List<int>();

            if (game.Validate(genreIds, platformIds))
            {
                _gameListViewModel.GameCollection.Add(game);

                foreach (var genre in vm.SelectedGenres)
                    AddGamePage.GameGenreCollection.Add(new GameGenre { GameID = game.ID, GenreID = genre.ID });

                if (vm.SelectedPlatform != null)
                    AddGamePage.GamePlatformCollection.Add(new GamePlatform { GameID = game.ID, GameConsoleID = vm.SelectedPlatform.ID });

                await DisplayAlert("Success", "Game saved successfully!", "OK");

                await Navigation.PopAsync();
            }
            else
            {
                string errorMsg = string.Join("\n", game.ValidationErrors ?? new List<string> { "Unknown error." });
                await DisplayAlert("Error", "Could not save the game.", "OK");
            }
        }
    }




    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this game?", "Yes", "No");
        if (confirm)
        {
            var vm = BindingContext as GameDetailsViewModel;
            var game = vm?.CurrentGame;
            if (game != null)
            {
                // Remove from main collection
                _gameListViewModel.GameCollection.Remove(game);

                // Remove from linking tables
                var genresToRemove = AddGamePage.GameGenreCollection.Where(gg => gg.GameID == game.ID).ToList();
                foreach (var gg in genresToRemove)
                    AddGamePage.GameGenreCollection.Remove(gg);

                var platformsToRemove = AddGamePage.GamePlatformCollection.Where(gp => gp.GameID == game.ID).ToList();
                foreach (var gp in platformsToRemove)
                    AddGamePage.GamePlatformCollection.Remove(gp);

                await DisplayAlert("Deleted", "Game deleted.", "OK");
            }

            await Navigation.PopAsync();
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancel", "Are you sure you want to cancel? Unsaved changes will be lost.", "Yes", "No");
        if (confirm)
        {
            await DisplayAlert("Cancelled", "Operation cancelled.", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void OnCreateSeriesClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Create Series", "Enter the new series name:");
        if (!string.IsNullOrWhiteSpace(result))
        {
            var vm = BindingContext as GameDetailsViewModel;
            if (vm != null)
            {
                var newSeries = new Series { ID = GenerateSeriesId(vm), Name = result };
                vm.AvailableSeries.Add(newSeries);
                vm.SelectedSeries = newSeries;
            }
        }
    }

    private int GenerateSeriesId(GameDetailsViewModel vm)
    {
        return vm.AvailableSeries.Any() ? vm.AvailableSeries.Max(s => s.ID) + 1 : 1;
    }

    private async void OnAddPlatformClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as GameDetailsViewModel;
        string result = await DisplayPromptAsync("Add Platform", "Enter the new platform name:");
        if (!string.IsNullOrWhiteSpace(result) && vm != null)
        {
            var newPlatform = new GameConsole { ID = GeneratePlatformId(vm), Name = result };
            vm.AvailablePlatforms.Add(newPlatform);
            // Optionally, select the new platform by default
            vm.SelectedPlatform = newPlatform;
        }
    }


    private int GeneratePlatformId(GameDetailsViewModel vm)
    {
        return vm.AvailablePlatforms.Any() ? vm.AvailablePlatforms.Max(p => p.ID) + 1 : 1;
    }


}