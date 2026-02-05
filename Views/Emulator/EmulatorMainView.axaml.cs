using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using GameZard.ViewModels.EmulatorViewModels;
using System;
using System.Threading.Tasks;
using ViewModels.EmulatorViewModels;

namespace GameZard.Views;

public partial class EmulatorMainView : UserControl
{
    private EmulatorMainViewModel _EmulatorMainViewModel;

    public EmulatorMainView()
    {
        InitializeComponent();
        //DataContext = new EmulatorMainViewModel();
        EmulatorMainViewModel = new EmulatorMainViewModel();
        //Once the UI is fully loaded, awaits the LoadEmulatorAtFirstLoad()
        this.Loaded += async (_, __) => await LoadEmulatorAtFirstLoad();
        DataContext = EmulatorMainViewModel;
    }

    public EmulatorMainViewModel EmulatorMainViewModel
    {
        get { return _EmulatorMainViewModel; }
        set { _EmulatorMainViewModel = value; }
    }

    #region Folder picker
    //Triggers the folder picker to select the FromPath folder
    private async void BrowseFromPathFolder_Click(Object? Sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        if (topLevel?.StorageProvider is not IStorageProvider storage)
            return;

        var folders = await storage.OpenFolderPickerAsync(

            new FolderPickerOpenOptions
            {
                Title = "Select From folder",
                AllowMultiple = false
            });

        if (folders.Count == 0)

            return;

        var selectedPath = folders[0].Path.LocalPath;

        //Assigning the selected path to the DTO's FromPath property
        if (DataContext is EmulatorMainViewModel evm)
        {
            evm.MainDomain.EmulatorSavedataDTO.FromPath = selectedPath;
        }

    }

    private async void BrowseToPathFolder_Click(Object? Sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        if (topLevel?.StorageProvider is not IStorageProvider storage)
            return;

        var folders = await storage.OpenFolderPickerAsync(

            new FolderPickerOpenOptions
            {
                Title = "Select To folder",
                AllowMultiple = false
            });

        if (folders.Count == 0)

            return;

        var selectedPath = folders[0].Path.LocalPath;

        //Assigning the selected path to the DTO's ToPath property
        if (DataContext is EmulatorMainViewModel evm)
        {
            evm.MainDomain.EmulatorSavedataDTO.ToPath = selectedPath;
        }

    }
    #endregion

    public async Task LoadEmulatorAtFirstLoad()
    {
        await EmulatorMainViewModel.MainDomain.DisplayEmulatorSavedataStartAsync();
    }
}