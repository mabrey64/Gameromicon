using Gameromicon.Pages;

namespace Gameromicon
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GameListPage), typeof(Pages.GameListPage));
            Routing.RegisterRoute(nameof(AddGamePage), typeof(Pages.AddGamePage));
        }
    }
}
