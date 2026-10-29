using Microsoft.Extensions.DependencyInjection;
using shopBL;
using shopDAL.Entities;
using shopDAL.Repositories;
using shopViewModels;
using System;
using System.Windows;

namespace shopWPF
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);

            Services = services.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Сталася помилка: {e.Exception.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TradingDbContext>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();

            services.AddTransient<LoginWindow>();
            services.AddTransient<RegistrationWindow>();

            services.AddTransient<PasswordRecoveryViewModel>();
            services.AddTransient<PasswordRecoveryWindow>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}