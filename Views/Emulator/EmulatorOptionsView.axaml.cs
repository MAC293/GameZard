using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GameZard.ViewModels.EmulatorViewModels;

namespace GameZard.Views;

public partial class EmulatorOptionsView : UserControl
{
    public EmulatorOptionsView()
    {
        InitializeComponent();
        DataContext = new EmulatorOptionsViewModel();
    }
}