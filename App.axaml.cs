using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GameZard.Domain;
using GameZard.ViewModels.EmulatorViewModels;
using GameZard.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using ViewModels.EmulatorViewModels;

namespace GameZard
{
    public partial class App : Application
    {
        //Implement DI Container later instead of static property
        //public static EmulatorMainViewModel MainViewModel { get; } = new();

        private IServiceProvider _ServiceProvider;

        public IServiceProvider ServiceProvider
        {
            get { return _ServiceProvider; } 
            set { _ServiceProvider = value; }
        }

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
            //Configure DI
            var services = new ServiceCollection();

            //Register EmulatorMainViewModel as Singleton
            services.AddSingleton<EmulatorMainViewModel>();

            //Register other VMs as needed  
            //services.AddTransient<EmulatorListViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                ServiceProvider.GetRequiredService<EmulatorMainViewModel>();

                desktop.MainWindow = new EmulatorView();
            }

            base.OnFrameworkInitializationCompleted();
        }

    }
}