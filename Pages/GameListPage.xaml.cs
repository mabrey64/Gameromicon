using Gameromicon.Classes;
using Gameromicon.ViewModels;

namespace Gameromicon.Pages;

public partial class GameListPage : ContentPage
{
    private readonly ViewModels.GameListViewModel _gameListViewModel = new();
    private Game? _selectedGame;

    public GameListPage()
	{
		InitializeComponent();
        BindingContext = _gameListViewModel;
	}

    // This method is called when the page appears
    public async void OnEditOrAddButtonClicked(object sender, EventArgs e)
    {
        if (_selectedGame != null)
        {
            await Navigation.PushAsync(new AddGamePage(_gameListViewModel, _selectedGame));
            // Optionally clear selection after editing
            _selectedGame = null;
            EditOrAddButton.Text = "+";
        }
        else
        {
            await Navigation.PushAsync(new AddGamePage(_gameListViewModel));
        }
    }

    // This method is called when a game is tapped in the list
    private async void OnGameTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Game tappedGame)
        {
            await Navigation.PushAsync(new AddGamePage(_gameListViewModel, tappedGame));
        }
    }

    // This method is called when the selection changes in the ListView
    private void OnGameSelected(object sender, SelectionChangedEventArgs e)
    {
        _selectedGame = e.CurrentSelection.FirstOrDefault() as Game;
        EditOrAddButton.Text = _selectedGame != null ? "Edit" : "+";
    }


}