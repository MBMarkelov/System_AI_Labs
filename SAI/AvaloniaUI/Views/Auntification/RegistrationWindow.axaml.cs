using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaUI;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow() => InitializeComponent();
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

}