namespace Gameromicon.Pages;

public partial class GameListPage : ContentPage
{
	public GameListPage()
	{
		InitializeComponent();
	}

	public async void OnAddGameButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddGamePage));
    }
}