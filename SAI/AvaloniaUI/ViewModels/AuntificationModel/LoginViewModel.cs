using Avalonia;
using Avalonia.Controls;
using AvaloniaUI.Views;
using ReactiveUI;
using SAI.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;


namespace AvaloniaUI.ViewModels.AuntificationModel
{
    public class LoginViewModel : ReactiveObject
    {
        public event Action RequestClose;
        private readonly Window _currentWindow;

        private string _phoneNumber;
        private string _errorMessage;
        private readonly AuthService _authService;

        public string PhoneNumber
        {
            get => _phoneNumber; 
            set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }
        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public LoginViewModel(AuthService authService, Window currentWindow)
        {
            _authService = authService;
            _currentWindow = currentWindow;
            LoginCommand = ReactiveCommand.CreateFromTask(LoginAsync);
        }
        private async Task LoginAsync()
        {
            ErrorMessage = string.Empty;
            var user = await _authService.LoginAsync(PhoneNumber);
            if (user == null)
            {
                var registerWindow = new RegistrationWindow();
                registerWindow.DataContext = new RegistrationViewModel(_authService, registerWindow);
                registerWindow.Show();
            }
            else
            {
                var main = new MainWindow();
                main.Show();
                _currentWindow.Close();
            }
        }
    }
}
