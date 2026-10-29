using shopViewModels;
using System.Windows;

namespace shopWPF
{
    public partial class PasswordRecoveryWindow : Window
    {
        public PasswordRecoveryWindow(PasswordRecoveryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.GetNewPassword = () => pwdNewPassword.Password;
            viewModel.GetConfirmPassword = () => pwdConfirmPassword.Password;

            viewModel.OnSuccess = () =>
            {
                MessageBox.Show("Пароль успішно змінено! Тепер ви можете увійти.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            };

            viewModel.OnCancel = () => this.Close();
        }
    }
}