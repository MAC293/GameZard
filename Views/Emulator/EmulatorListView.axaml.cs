using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GameZard.Services;
using GameZard.ViewModels.EmulatorViewModels;
using ViewModels.EmulatorViewModels;

namespace GameZard.Views;

public partial class EmulatorListView : UserControl
{
    private EmulatorViewModel _EmulatorViewModel;
    private EmulatorListViewModel _EmulatorListViewModel;

    public EmulatorListView()
    {
        InitializeComponent();
        //EmulatorViewModel = new EmulatorViewModel();
        EmulatorListViewModel = new EmulatorListViewModel();
        LoadListSelected();
        //DataContext = EmulatorViewModel;
        DataContext = EmulatorListViewModel;
    }

    public EmulatorViewModel EmulatorViewModel
    {
        get { return _EmulatorViewModel; }
        set { _EmulatorViewModel = value; }
    }

    public EmulatorListViewModel EmulatorListViewModel
    {
        get { return _EmulatorListViewModel; }
        set { _EmulatorListViewModel = value; }
    }

    public void LoadListSelected()
    {
        EmulatorListViewModel.ListDomain.LoadEmulatorsAtStart();
    }
}