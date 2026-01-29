using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using GameZard.ViewModels.EmulatorViewModels;

namespace GameZard.Views;

public partial class EmulatorMainView : UserControl
{
    public EmulatorMainView()
    {
        InitializeComponent();
        DataContext = new EmulatorMainViewModel();
    }

    private async void BrowseFromFolder_Click(Object? Sender, RoutedEventArgs e)
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

        //Invoking a command in the ViewModel and passing data to it
        if (DataContext is EmulatorMainViewModel evm)
        {
            evm.MainDomain.EmulatorSavedataDTO.FromPath = selectedPath;
        }

    }
}