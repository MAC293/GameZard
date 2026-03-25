using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameZard.Domain;
using GameZard.DTO;
using GameZard.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.ViewModels.EmulatorViewModels
{
    public partial class EmulatorOptionsViewModel
    {
        private OptionsDomain _OptionsDomain;
        private EmulatorSavedataDTO _EmulatorSavedataDTO;
        private String _TargetFolderPath;

        public EmulatorOptionsViewModel()
        {
            OptionsDomain = new OptionsDomain();

            WeakReferenceMessenger.Default.Register<EmulatorMainMessage>(this, async (recipient, message) =>
            {
                if (!String.IsNullOrWhiteSpace(message.SelectedEmulator))
                {
                    String shownEmulator = NameFormatter.UnformatEmulatorName(message.SelectedEmulator);

                    String targetFolder = await OptionsDomain.TargetPathAsync(shownEmulator.Trim());

                    TargetFolderPath = targetFolder;
                }
            });
        }

        public OptionsDomain OptionsDomain
        {
            get { return _OptionsDomain; }
            set { _OptionsDomain = value; }
        }
        
        public EmulatorSavedataDTO EmulatorSavedataDTO
        {
            get { return _EmulatorSavedataDTO; }
            set { _EmulatorSavedataDTO = value; }
        }
        
        public String TargetFolderPath
        {
            get { return _TargetFolderPath; }
            set { _TargetFolderPath = value; }
        }

        //Log.Information($"SelectedEmulatorAsync(): {emulatorSelectedModel}");
        #region Remove Backup Command
        public async Task<Boolean> CanRemoveBackup()
        {
            //Log.Information($"TargetFolderPath: {TargetFolderPath}");

            //Check if folder has any content within
            Boolean hasContent = Directory.EnumerateFileSystemEntries(TargetFolderPath).Any();

            if (hasContent)
            {
                return true;
            }

            return false;
            
        }

        [RelayCommand]
        public async Task RemoveBackup()
        {
            if (await CanRemoveBackup())
            {
               await ClearTargetFolder();
            }
        }

        public async Task ClearTargetFolder()
        {
            //Delete all files
            foreach (String file in Directory.GetFiles(TargetFolderPath))
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            //Delete all subdirectories
            foreach (String dir in Directory.GetDirectories(TargetFolderPath))
            {
                Directory.Delete(dir, recursive: true);
            }

        }
        #endregion

    }
}
