using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameZard.Domain;
using GameZard.DTO;
using GameZard.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using Serilog;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.ViewModels.EmulatorViewModels
{
    public partial class EmulatorMainViewModel
    {
        private MainDomain _MainDomain;

        public EmulatorMainViewModel()
        {
            MainDomain = new MainDomain();
            PropertyChangedBackupMode();
            PropertyChangedFromPath();
            PropertyChangedToPath();

            WeakReferenceMessenger.Default.Register<EmulatorMainMessage>(this, async (recipient, message) =>
            {
                if (!String.IsNullOrWhiteSpace(message.SelectedEmulator))
                {
                    //Log.Information($"Current Emulator: {message.SelectedEmulator}");
                    await LoadEmulatorSavedataAsync(NameFormatter.UnformatEmulatorName(message.SelectedEmulator));
                }
            });
        }

        public MainDomain MainDomain
        {
            get { return _MainDomain; }
            set { _MainDomain = value; }
        }

        //Display selected emulator savedata
        public async Task LoadEmulatorSavedataAsync(String selectedEmulator)
        {
            await MainDomain.DisplayEmulatorSavedataAsync(selectedEmulator);
        }

        #region Backup command

        //Perform every folder validation
        public Boolean CanBackupNow()
        {

            if (BackupEngine.TargetFolderExists(MainDomain.EmulatorSavedataDTO.ToPath) && BackupEngine.HasWritePermission(MainDomain.EmulatorSavedataDTO.ToPath))
            {
                return true;
            }

            return false;
        }

        //Perform the copy operation
        [RelayCommand]
        public async Task BackupNow()
        {
            if (CanBackupNow()) 
            {
                //Log.Information("Backing up!");
                await BackupEngine.BackupNowAsync(MainDomain.EmulatorSavedataDTO.FromPath.Trim(), MainDomain.EmulatorSavedataDTO.ToPath.Trim());

                String currentEmulator = NameFormatter.UnformatCurrentEmulatorID(MainDomain.EmulatorSavedataDTO.ID.Trim());

                String lastSave = BackupEngine.LastSaveTimeDate(DateTime.Now);

                await MainDomain.MainModel.UpdateLastSaveAsync(currentEmulator, lastSave);

                MainDomain.EmulatorSavedataDTO.LastSave = lastSave;

                return;
            }

            Log.Information("Not Backing up!");
        }
        #endregion

        #region Radio Button command
        [RelayCommand]
        public async Task SelectBackupMode()
        {
            //Automatically
            //Log.Information($"Selected backup mode: {MainDomain.EmulatorSavedataDTO.BackUpMode}");
            
            //PPSSPP
            //Log.Information($"Selected backup mode: {MainDomain.EmulatorSavedataDTO.ID}");

            await MainDomain.MainModel.UpdateBackupModeAsync(NameFormatter.UnformatEmulatorName(MainDomain.EmulatorSavedataDTO.ID.Trim()), MainDomain.EmulatorSavedataDTO.BackUpMode);

        }

        private void PropertyChangedBackupMode()
        {
            MainDomain.EmulatorSavedataDTO.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(MainDomain.EmulatorSavedataDTO.BackUpMode))
                {
                    SelectBackupModeCommand.NotifyCanExecuteChanged();
                }
            };
        }
        #endregion

        #region From Path command
        public Boolean CanAddFromPath()
        {
            if (MainDomain.EmulatorSavedataDTO != null)
            {
                if (!String.IsNullOrEmpty(MainDomain.EmulatorSavedataDTO.FromPath))
                {
                    if (MainDomain.EmulatorSavedataDTO.FromPath.Length <= 250)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //[RelayCommand(CanExecute = nameof(CanAddFromPath))]
        public async Task AddFromPath()
        {
            await MainDomain.MainModel.UpdateFromPathAsync(NameFormatter.UnformatEmulatorName(MainDomain.EmulatorSavedataDTO.ID.Trim()), MainDomain.EmulatorSavedataDTO.FromPath);
        }

        private void PropertyChangedFromPath()
        {
            MainDomain.EmulatorSavedataDTO.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(MainDomain.EmulatorSavedataDTO.FromPath))
                {
                    //AddFromPathCommand.NotifyCanExecuteChanged();
                    if (CanAddFromPath())
                    { 
                      await AddFromPath();
                    }

                }
            };
        }
        #endregion

        #region To Path command
        public Boolean CanAddToPath()
        {
            if (MainDomain.EmulatorSavedataDTO != null)
            {
                if (!String.IsNullOrEmpty(MainDomain.EmulatorSavedataDTO.ToPath))
                {
                    if (MainDomain.EmulatorSavedataDTO.ToPath.Length <= 250)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task AddToPath()
        {
            await MainDomain.MainModel.UpdateToPathAsync(NameFormatter.UnformatEmulatorName(MainDomain.EmulatorSavedataDTO.ID.Trim()), MainDomain.EmulatorSavedataDTO.ToPath);
        }

        private void PropertyChangedToPath()
        {
            MainDomain.EmulatorSavedataDTO.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(MainDomain.EmulatorSavedataDTO.ToPath))
                {
                    if (CanAddToPath())
                    {
                        await AddToPath();
                    }

                }
            };
        }
        #endregion
    }
}
