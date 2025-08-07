using Gameromicon.ViewModels;
using System.ComponentModel;

namespace Gameromicon.Pages;

public partial class SignUpPage : ContentPage
{
    private ViewModels.AuthViewModel vm;
	public SignUpPage(AuthViewModel sharedViewModel)
	{
		InitializeComponent();
        vm = sharedViewModel;
        BindingContext = vm;
        vm.PropertyChanged += ViewModel_PropertyChanged;

        vm.IsSignUpMode = true; // Ensure we are in sign-up mode
        vm.ClearAllErrors(); // Clear any previous errors
    }

    private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(vm.IsSignUpSuccessful) && vm.IsSignUpSuccessful)
        {
            await DisplayAlert("Success", "Sign up successful!", "OK");
            vm.IsSignUpSuccessful = false; // Reset flag
            await Navigation.PopAsync();    // Go back to sign-in page
        }
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}