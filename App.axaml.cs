using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GameZard.Views;

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

            /*
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            { 
                desktop.MainWindow = new EmulatorView();
                {
                    DataContext = EmulatorSelectorViewModel;
                };

            }
            */

            base.OnFrameworkInitializationCompleted();
        }
    }
}