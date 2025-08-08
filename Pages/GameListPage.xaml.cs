using Gameromicon.Classes;
using Gameromicon.ViewModels;

namespace Gameromicon.Pages;

public partial class GameListPage : ContentPage
{
    private readonly ViewModels.GameListViewModel _gameListViewModel = new();
    public GameListPage()
	{
		InitializeComponent();
        BindingContext = _gameListViewModel;
	}


    public async void OnAddGameButtonClicked(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Add Game button clicked");
        await Navigation.PushAsync(new AddGamePage(_gameListViewModel));
    }

    private async void OnGameSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Game selectedGame)
        {
            await Navigation.PushAsync(new AddGamePage(BindingContext as GameListViewModel, selectedGame));
            ((CollectionView)sender).SelectedItem = null; // Optional: clear selection
        }
    }

}