using System.ComponentModel;

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
            if (vm != null)
            {
                vm.PropertyChanged += ViewModel_PropertyChanged;
            }
        }
        private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(vm.IsLoginSuccessful) && vm.IsLoginSuccessful)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                // Optionally navigate to the next page here
                vm.IsLoginSuccessful = false; // Reset if needed
            }
        }
    }

}
