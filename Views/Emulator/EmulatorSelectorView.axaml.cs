using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ViewModels.EmulatorViewModels;

namespace GameZard.Views;

public partial class EmulatorSelectorView : UserControl
{
    private EmulatorSelectorViewModel _EmulatorSelectorViewModel;

    public EmulatorSelectorView()
    {
        InitializeComponent();

        EmulatorSelectorViewModel = new EmulatorSelectorViewModel();
        LoadSelector();
        DataContext = EmulatorSelectorViewModel;
    }

    public EmulatorSelectorViewModel EmulatorSelectorViewModel
    {
        get { return _EmulatorSelectorViewModel; }
        set { _EmulatorSelectorViewModel = value; }
    }

    public void LoadSelector()
    {
        EmulatorSelectorViewModel.SelectorDomain.LoadEmulators();
        EmulatorSelectorViewModel.SelectorDomain.EmulatorDTO.Emulators = EmulatorSelectorViewModel.FormattedEmulators();
        EmulatorSelectorViewModel.SelectorDomain.EmulatorsPlaceholder();

    }
}