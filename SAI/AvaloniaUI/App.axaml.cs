using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaUI;
using AvaloniaUI.ViewModels.AuntificationModel;
using Microsoft.Extensions.DependencyInjection;
using SAI.Application.Services;
using System;
using System.Net.Http;

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
        }
    }
}
