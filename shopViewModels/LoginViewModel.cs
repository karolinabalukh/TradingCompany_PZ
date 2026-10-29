using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using shopBL;
using System;

namespace shopViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _login = string.Empty;

        [ObservableProperty]
        private string? _errorMessage;

        public Func<string> GetPassword { get; set; } = null!;
        public Action OnLoginSuccess { get; set; } = null!;
        public Action OnLoginFailed { get; set; } = null!;
        public Action OnOpenRegistration { get; set; } = null!;
        public Action OnOpenRecovery { get; set; } = null!;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private void SignIn()
        {
            string password = GetPassword?.Invoke() ?? "";

            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Введіть логін та пароль.";
                return;
            }

            if (_authService.Login(Login, password))
            {
                ErrorMessage = null;
                OnLoginSuccess?.Invoke();
            }
            else
            {
                ErrorMessage = "Невірний логін або пароль.";
                OnLoginFailed?.Invoke();
            }
        }

        [RelayCommand]
        private void OpenRegistration() => OnOpenRegistration?.Invoke();

        [RelayCommand]
        private void OpenRecovery() => OnOpenRecovery?.Invoke();
    }
}