using shopViewModels;
using System.Windows;

namespace shopWPF
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.GetPassword = () => pwdPassword.Password;

            viewModel.OnLoginSuccess = () =>
            {
                MessageBox.Show("Ви успішно увійшли в систему!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); 
            };


            viewModel.OnLoginFailed = () =>
            {
                pwdPassword.Clear();
                pwdPassword.Focus();

                if (MessageBox.Show("Невірний логін або пароль.\nБажаєте відновити пароль?", "Помилка", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    viewModel.OpenRecoveryCommand.Execute(null);
                }
            };


            viewModel.OnOpenRegistration = () =>
            {
                var regWindow = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<RegistrationWindow>(App.Services);
                regWindow.Owner = this;
                regWindow.ShowDialog();
            };

            viewModel.OnOpenRecovery = () =>
            {
                var recWindow = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<PasswordRecoveryWindow>(App.Services);
                recWindow.Owner = this;
                recWindow.ShowDialog();
            };
        }
    }
}