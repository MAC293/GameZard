using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GameZard.ViewModels.EmulatorViewModels;
using GameZard.Views;
using Serilog;
using ViewModels.EmulatorViewModels;

namespace GameZard
{
    public partial class App : Application
    {
        //Implement DI Container later instead of static property
        //public static EmulatorMainViewModel MainViewModel { get; } = new();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.Information()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                //.WriteTo.File("E:/Programming/Business Projects/GameZard Project/Log.txt")
                .CreateLogger();

            //Log.Information("Log initialized");
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