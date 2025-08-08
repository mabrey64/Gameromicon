using Gameromicon.Pages;

namespace Gameromicon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Pages.AuthPage());
            Routing.RegisterRoute(nameof(AddGamePage), typeof(AddGamePage));
        }
    }
} 