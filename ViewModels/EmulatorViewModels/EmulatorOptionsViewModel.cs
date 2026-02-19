using CommunityToolkit.Mvvm.Input;
using GameZard.Domain;
using GameZard.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.Services;
using Serilog;

namespace GameZard.ViewModels.EmulatorViewModels
{
    public partial class EmulatorOptionsViewModel
    {
        private OptionsDomain _OptionsDomain;
        private EmulatorSavedataDTO _EmulatorSavedataDTO;

        public EmulatorOptionsViewModel()
        {
            OptionsDomain = new OptionsDomain();
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

        //Log.Information($"SelectedEmulatorAsync(): {emulatorSelectedModel}");
        #region Remove Backup Command
        public async Task<Boolean> CanRemoveBackup()
        {
            //If I come to get path from the EmulatorSavedataDTO, instead from the database
            //String targetFolderPath = EmulatorSavedataDTO.ToPath;

            //If I have to get path from database instead from the View
            Log.Information($"EmulatorSavedataDTO: {EmulatorDomain.Emulator.Name}");

            String shownEmulator = NameFormatter.UnformatEmulatorName(EmulatorSavedataDTO.Emulator);

            String targetFolder = await OptionsDomain.TargetPathAsync(shownEmulator.Trim());
            

            //Check if folder has any content within
            Boolean hasContent = Directory.EnumerateFileSystemEntries(targetFolder).Any();

            if (hasContent)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            foreach (String file in Directory.GetFiles(EmulatorSavedataDTO.ToPath.Trim()))
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            //Delete all subdirectories
            foreach (String dir in Directory.GetDirectories(EmulatorSavedataDTO.ToPath.Trim()))
            {
                Directory.Delete(dir, recursive: true);
            }

        }
        #endregion

    }
}
