using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gameromicon.Classes ;
using Microsoft.Maui.Controls;

namespace Gameromicon.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private bool IsValid;
        private string ProfilePassword;
        private string ProfileEmail;
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                IsEmailErrorVisible = true;
                ((Command)SignInCommand).ChangeCanExecute();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                IsPasswordErrorVisible = true;
                PasswordErrorMessage = string.Empty;
                ((Command)SignInCommand).ChangeCanExecute();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                IsConfirmPasswordErrorVisible = true;
                ConfirmPasswordErrorMessage = string.Empty;
                ((Command)SignInCommand).ChangeCanExecute();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        // UI State Properties
        private bool _isSignUpMode;
        public bool IsSignUpMode
        {
            get => _isSignUpMode;
            set
            {
                _isSignUpMode = value;
                OnPropertyChanged();

            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                    // Notify commands can execute changed
                    ((Command)SignInCommand).ChangeCanExecute();
                    ((Command)SignUpCommand).ChangeCanExecute();
                }
            }
        }

        private bool _isLoginSuccessful;
        public bool IsLoginSuccessful
        {
            get => _isLoginSuccessful;
            set
            {
                if (_isLoginSuccessful != value)
                {
                    _isLoginSuccessful = value;
                    OnPropertyChanged();
                }
            }
        }

        // Error Message Properties
        private bool _isEmailErrorVisible;
        public bool IsEmailErrorVisible
        {
            get => _isEmailErrorVisible;
            set { _isEmailErrorVisible = value; OnPropertyChanged(); }
        }
        private string _emailErrorMessage;
        public string EmailErrorMessage
        {
            get => _emailErrorMessage;
            set { _emailErrorMessage = value; OnPropertyChanged(); }
        }

        private bool _isPasswordErrorVisible;
        public bool IsPasswordErrorVisible
        {
            get => _isPasswordErrorVisible;
            set { _isPasswordErrorVisible = value; OnPropertyChanged(); }
        }
        private string _passwordErrorMessage;
        public string PasswordErrorMessage
        {
            get => _passwordErrorMessage;
            set { _passwordErrorMessage = value; OnPropertyChanged(); }
        }

        private bool _isConfirmPasswordErrorVisible;
        public bool IsConfirmPasswordErrorVisible
        {
            get => _isConfirmPasswordErrorVisible;
            set { _isConfirmPasswordErrorVisible = value; OnPropertyChanged(); }
        }
        private string _confirmPasswordErrorMessage;
        public string ConfirmPasswordErrorMessage
        {
            get => _confirmPasswordErrorMessage;
            set { _confirmPasswordErrorMessage = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public AuthViewModel()
        {
            IsSignUpMode = false; // Default to Sign In mode
            SignInCommand = new Command(async () => await ExecuteSignInAsync(), CanExecuteSignIn);
            SignUpCommand = new Command(async () => await ExecuteSignUpAsync(), CanExecuteSignUp);
            ToggleSignUpCommand = new Command(() => IsSignUpMode = !IsSignUpMode);

            IsSignUpMode = false; // Default to Sign In mode
            ClearAllErrors();
        }
        private void ClearAllErrors()
        {
            IsEmailErrorVisible = false;
            EmailErrorMessage = string.Empty;
            IsPasswordErrorVisible = false;
            PasswordErrorMessage = string.Empty;
            IsConfirmPasswordErrorVisible = false;
            ConfirmPasswordErrorMessage = string.Empty;
        }
        private void ClearAllFields()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
        private async Task ExecuteSignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            try
            {
                ClearAllErrors();
                // Validate input
                if (string.IsNullOrWhiteSpace(Email))
                {
                    IsEmailErrorVisible = true;
                    EmailErrorMessage = "Email is required.";
                    IsValid = false;
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    IsPasswordErrorVisible = true;
                    PasswordErrorMessage = "Password is required.";
                    IsValid = false;
                }
                if (ProfilePassword != Password)
                {
                    IsPasswordErrorVisible = true;
                    PasswordErrorMessage = "Wrong password, please type in the correct password";
                    IsValid = false;
                }
                if (ProfileEmail != Email)
                {
                    IsEmailErrorVisible = true;
                    EmailErrorMessage = "There is no user with that email, please try a different email.";
                    IsValid = false;
                }
                if(!IsValid)
                {
                    return;
                }
                // Call your authentication service here
                // await AuthService.SignInAsync(Email, Password);
                // On success, navigate to the next page or show success message
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in SignIn/SignUp: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
            IsLoginSuccessful = true;
        }
        private async Task ExecuteSignUpAsync()
        {
            IsValid = true;
            IsBusy = true;
            try
            {
                ClearAllErrors();
                // Validate input
                if (string.IsNullOrWhiteSpace(Email))
                {
                    IsEmailErrorVisible = true;
                    EmailErrorMessage = "Email is required.";
                    IsValid = false;
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    IsPasswordErrorVisible = true;
                    PasswordErrorMessage = "Password is required.";
                    IsValid = false;
                }
                if (string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    IsConfirmPasswordErrorVisible = true;
                    ConfirmPasswordErrorMessage = "Confirm Password is required.";
                    IsValid = false;
                }
                if (Password != ConfirmPassword)
                {
                    IsConfirmPasswordErrorVisible = true;
                    ConfirmPasswordErrorMessage = "Passwords do not match.";
                    IsValid = false;
                }
                if (!IsValid)
                {
                    return;
                }
                // Call your authentication service here
                // await AuthService.SignUpAsync(Email, Password, Username);
                // On success, navigate to the next page or show success message
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in SignIn/SignUp: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
            ProfileEmail = Email;
            ProfilePassword = Password;
            IsSignUpMode = false;
            ClearAllFields();
            Debug.WriteLine("SignUpCommand executed!");
        }
        private bool CanExecuteSignIn()
        {
            return !IsBusy && !string.IsNullOrEmpty(ProfileEmail) && !string.IsNullOrEmpty(ProfilePassword);
        }
        private bool CanExecuteSignUp()
        {
            return !IsBusy;
        }
        public ICommand ToggleSignUpCommand { get; private set; }
    }
}