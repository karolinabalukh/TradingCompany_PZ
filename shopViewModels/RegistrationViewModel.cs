using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using shopBL;
using shopDTO;
using System;

namespace shopViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _login = string.Empty;

        [ObservableProperty]
        private string _firstName = string.Empty;

        [ObservableProperty]
        private string _lastName = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phone = string.Empty;

        [ObservableProperty]
        private string _gender = string.Empty;

        [ObservableProperty]
        private string _address = string.Empty;

        [ObservableProperty]
        private string _cardNumber = string.Empty;

        [ObservableProperty]
        private string _secretWord = string.Empty;

        [ObservableProperty]
        private string? _errorMessage;

        public Func<string> GetPassword { get; set; } = null!;
        public Func<string> GetConfirmPassword { get; set; } = null!;
        public Action OnSuccess { get; set; } = null!;
        public Action OnCancel { get; set; } = null!;

        public RegistrationViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private void Register()
        {
            string password = GetPassword?.Invoke() ?? "";
            string confirmPassword = GetConfirmPassword?.Invoke() ?? "";

            var userDto = new UserDTO
            {
                Login = this.Login,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Phone = this.Phone,
                Gender = this.Gender,
                Address = this.Address,
                SecretWord = this.SecretWord
            };

            var cardDto = new BankCardDTO { Number = this.CardNumber };

            try
            {
                _authService.RegisterGuest(userDto, cardDto, password, confirmPassword);
                ErrorMessage = null;
                OnSuccess?.Invoke();
            }
            catch (BusinessRuleException ex)
            {
                ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                ErrorMessage = "Сталася непередбачена помилка під час реєстрації.";
            }
        }

        [RelayCommand]
        private void Cancel() => OnCancel?.Invoke();
    }
}