using System.ComponentModel;
using System.Diagnostics;

namespace Gameromicon.Pages
{
    public partial class AuthPage : ContentPage
    {
        private ViewModels.AuthViewModel vm;
        public AuthPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.AuthViewModel();
            vm = BindingContext as ViewModels.AuthViewModel;
            vm.PropertyChanged += ViewModel_PropertyChanged;
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage(vm));
        }

        private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine($"[AuthPage] PropertyChanged: {e.PropertyName}, Value: {vm.IsLoginSuccessful}");
            if (e.PropertyName == nameof(vm.IsLoginSuccessful) && vm.IsLoginSuccessful)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                try
                {
                    Debug.WriteLine($"Shell.Current is null: {Shell.Current == null}");
                    await Navigation.PushAsync(new GameListPage());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation Exception: {ex}");
                    await DisplayAlert("Navigation Exception", ex.ToString(), "OK");
                }
            }
        }

    }

}
