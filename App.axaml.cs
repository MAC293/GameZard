using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GameZard.ViewModels.EmulatorViewModels;
using GameZard.Views;
using ViewModels.EmulatorViewModels;

namespace GameZard
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new EmulatorView();

            }

            base.OnFrameworkInitializationCompleted();
        }

    }
}