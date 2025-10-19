using Avalonia.Controls;
using AvaloniaUI.Views;
using ReactiveUI;
using SAI.Application.DTOs;
using SAI.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaUI.ViewModels.AuntificationModel
{
    public class RegistrationViewModel : ReactiveObject
    {
        private readonly AuthService _authService;
        private readonly Window _currentWindow;
        public UserDto User { get; set; } = new();
        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
        public RegistrationViewModel(AuthService authService, Window CurrentWindow)
        {
            _authService = authService;
            _currentWindow = CurrentWindow;
            RegisterCommand = ReactiveCommand.CreateFromTask(RegisterAsync);
        }
        private async Task RegisterAsync()
        {
            var success = await _authService.ResgisterAsync(User);
            if (success)
            {
                var main = new MainWindow();
                main.Show();
                _currentWindow.Close();
            }
        }
    }   
}
