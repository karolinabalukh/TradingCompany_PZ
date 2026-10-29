using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using shopBL;
using System;

namespace shopViewModels
{
    public partial class PasswordRecoveryViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _login = string.Empty;

        [ObservableProperty]
        private string _secretWord = string.Empty;

        [ObservableProperty]
        private string? _errorMessage;

        public Func<string> GetNewPassword { get; set; } = null!;
        public Func<string> GetConfirmPassword { get; set; } = null!;
        public Action OnSuccess { get; set; } = null!;
        public Action OnCancel { get; set; } = null!;

        public PasswordRecoveryViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private void Recover()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(SecretWord))
            {
                ErrorMessage = "Заповніть логін та секретне слово.";
                return;
            }

            string newPassword = GetNewPassword?.Invoke() ?? "";
            string confirmPassword = GetConfirmPassword?.Invoke() ?? "";

            if (newPassword != confirmPassword)
            {
                ErrorMessage = "Нові паролі не співпадають.";
                return;
            }

            try
            {
                bool success = _authService.RecoverPassword(Login, SecretWord, newPassword);
                if (success)
                {
                    ErrorMessage = null;
                    OnSuccess?.Invoke();
                }
                else
                {
                    ErrorMessage = "Невірний логін або секретне слово.";
                }
            }
            catch (BusinessRuleException ex)
            {
                ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                ErrorMessage = "Сталася помилка при відновленні пароля.";
            }
        }

        [RelayCommand]
        private void Cancel() => OnCancel?.Invoke();
    }
}