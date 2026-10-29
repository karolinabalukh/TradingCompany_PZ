using shopViewModels;
using System.Windows;

namespace shopWPF
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow(RegistrationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.GetPassword = () => pwdPassword.Password;
            viewModel.GetConfirmPassword = () => pwdConfirmPassword.Password;

            viewModel.OnSuccess = () =>
            {
                MessageBox.Show("Реєстрація пройшла успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            };

            viewModel.OnCancel = () => this.Close();
        }
    }
}