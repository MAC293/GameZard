using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GameZard.ViewModels.EmulatorViewModels;
using ViewModels.EmulatorViewModels;

namespace GameZard.Views;

public partial class EmulatorListView : UserControl
{
    private EmulatorViewModel _EmulatorViewModel;

    public EmulatorListView()
    {
        InitializeComponent();
        EmulatorViewModel = new EmulatorViewModel();
        DataContext = EmulatorViewModel;
    }

    public EmulatorViewModel EmulatorViewModel
    {
        get { return _EmulatorViewModel; }
        set { _EmulatorViewModel = value; }
    }
}