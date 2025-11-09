using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
<<<<<<< Updated upstream
=======
using AvaloniaUI;
using AvaloniaUI.ViewModels.AuntificationModel;
using Microsoft.Extensions.DependencyInjection;
using SAI.Application.Services;
using System;
using System.Globalization;
using System.Net.Http;
>>>>>>> Stashed changes

namespace AvaloniaUI
{
    public partial class App : Avalonia.Application
    {
        private ServiceProvider _serviceProvider;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
<<<<<<< Updated upstream
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
=======
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");
            var services = new ServiceCollection();
            services.AddSingleton<HttpClient>();
            services.AddTransient<AuthService>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();

            _serviceProvider = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var authService = _serviceProvider.GetRequiredService<AuthService>();
                var loginWindow = new LoginWindow();
                var loginViewModel = new LoginViewModel(authService, loginWindow);

                desktop.MainWindow = new LoginWindow
                {
                    DataContext = loginViewModel
                };
            }

            base.OnFrameworkInitializationCompleted();

>>>>>>> Stashed changes
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
