using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gameromicon.Classes;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Gameromicon.ViewModels
{
    public partial class AuthViewModel : BaseViewModel
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;
        [ObservableProperty] private string confirmPassword;
        [ObservableProperty] private bool isSignUpMode;
        [ObservableProperty] private bool isBusy;
        [ObservableProperty] private bool isLoginSuccessful;
        [ObservableProperty] private bool isSignUpSuccessful;
        [ObservableProperty] private bool isNameErrorVisible;
        [ObservableProperty] private string nameErrorMessage;
        [ObservableProperty] private bool isEmailErrorVisible;
        [ObservableProperty] private string emailErrorMessage;
        [ObservableProperty] private bool isPasswordErrorVisible;
        [ObservableProperty] private string passwordErrorMessage;
        [ObservableProperty] private bool isConfirmPasswordErrorVisible;
        [ObservableProperty] private string confirmPasswordErrorMessage;

        public bool IsSignInMode => !IsSignUpMode;

        // Handles the SignIn action
        [RelayCommand]
        private async Task SignInAsync()
        {
            IsBusy = true;
            try
            {
                ClearAllErrors();
                bool isValid = true;

                // Retrieve saved credentials
                var savedEmail = await SecureStorage.Default.GetAsync("UserEmail");
                var savedPassword = await SecureStorage.Default.GetAsync("UserPassword");

                Debug.WriteLine($"Saved Email: {savedEmail}");
                Debug.WriteLine($"Saved Password: {savedPassword}");

                // Validate input
                if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                {
                    IsEmailErrorVisible = true;
                    EmailErrorMessage = "Please enter a valid email.";
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
                {
                    IsPasswordErrorVisible = true;
                    PasswordErrorMessage = "Password must be at least 6 characters.";
                    isValid = false;
                }

                // Check credentials if input is valid
                if (isValid)
                {
                    if (Email == savedEmail && Password == savedPassword)
                    {
                        Debug.WriteLine("SignIn worked! Credentials matched.");
                        Debug.WriteLine("SignInAsync executed");
                        await Task.Delay(1500);
                        IsLoginSuccessful = true;
                        OnPropertyChanged(nameof(IsLoginSuccessful));
                        ClearAllFields();
                    }
                    else
                    {
                        Debug.WriteLine("SignIn failed! Credentials did not match.");
                        IsEmailErrorVisible = true;
                        EmailErrorMessage = "Invalid email or password.";
                        IsLoginSuccessful = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SignInAsync Exception: {ex.Message}");
                IsLoginSuccessful = false;
            }
            finally
            {
                IsBusy = false;
            }
        }


        // Command to handle Sign Up action
        [RelayCommand]
        private async Task SignUpAsync()
        {
            Debug.WriteLine("SignUpAsync executed");

            IsBusy = true;
            try
            {
                ClearAllErrors();
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(Name))
                {
                    IsNameErrorVisible = true;
                    NameErrorMessage = "Name cannot be empty.";
                    Debug.WriteLine($"Name errpr set: {IsNameErrorVisible}, {NameErrorMessage}");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                {
                    IsEmailErrorVisible = true;
                    EmailErrorMessage = "Please enter a valid email.";
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
                {
                    IsPasswordErrorVisible = true;
                    PasswordErrorMessage = "Password must be at least 6 characters.";
                    isValid = false;
                }
                if (Password != ConfirmPassword)
                {
                    IsConfirmPasswordErrorVisible = true;
                    ConfirmPasswordErrorMessage = "Passwords do not match.";
                    isValid = false;
                }
                if (!isValid)
                {
                    IsSignUpSuccessful = false;
                    return;
                }
                await Task.Delay(1500);
                IsLoginSuccessful = true;

                // Save credentials to secure storage
                await SecureStorage.Default.SetAsync("UserEmail", Email);
                await SecureStorage.Default.SetAsync("UserPassword", Password);

                IsSignUpSuccessful = true;
                OnPropertyChanged(nameof(IsSignUpSuccessful));
                ClearAllFields();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SignUpAsync Exception: {ex.Message}");
                IsSignUpSuccessful = false;
                IsLoginSuccessful = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Method to clear all error messages and visibility flags
        public void ClearAllErrors()
        {
            IsNameErrorVisible = false;
            NameErrorMessage = string.Empty;
            IsEmailErrorVisible = false;
            EmailErrorMessage = string.Empty;
            IsPasswordErrorVisible = false;
            PasswordErrorMessage = string.Empty;
            IsConfirmPasswordErrorVisible = false;
            ConfirmPasswordErrorMessage = string.Empty;
        }

        // Method to clear all input fields
        public void ClearAllFields()
        {
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            Name = string.Empty;
        }

        // Method to toggle between Sign In and Sign Up modes
        [RelayCommand]
        private void ToggleSignUp()
        {
            Debug.WriteLine("ToggleSignUp called");
            IsSignUpMode = true;
            ClearAllErrors();
            ClearAllFields();
        }
    }
}
