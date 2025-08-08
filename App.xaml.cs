using Gameromicon.Pages;

namespace Gameromicon
{
    public partial class App : Application
    {
        public App()
        {
            Microsoft.Maui.Storage.SecureStorage.Default.SetAsync("UserEmail", "test@mail.com").Wait();
            Microsoft.Maui.Storage.SecureStorage.Default.SetAsync("UserPassword", "password123").Wait();
            InitializeComponent();
            MainPage = new NavigationPage(new Pages.AuthPage());
            Routing.RegisterRoute(nameof(AddGamePage), typeof(AddGamePage));
        }
    }
} 