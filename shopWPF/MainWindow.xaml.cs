using shopViewModels;
using System.Windows;
using Microsoft.Extensions.DependencyInjection; 

namespace shopWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.OnOpenLogin = () =>
            {
                var loginWindow = App.Services.GetRequiredService<LoginWindow>();
                loginWindow.Owner = this;
                loginWindow.ShowDialog();
            };
        }
    }
}