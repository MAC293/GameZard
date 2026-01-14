using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GameZard.ViewModels.EmulatorViewModels;

namespace GameZard.Views;

public partial class EmulatorMainView : UserControl
{
    public EmulatorMainViewModel _EmulatorMainViewModel;
    public EmulatorMainView()
    {
        //EmulatorMainViewModel = new EmulatorMainViewModel();
        InitializeComponent();
    }

    //public EmulatorMainViewModel EmulatorMainViewModel
    //{
    //    get { return _EmulatorMainViewModel; }
    //    set { _EmulatorMainViewModel = value; }
    //}

}