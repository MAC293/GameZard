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
            PropertyChangedName();
            PropertyChangedFromPath();
            PropertyChangedToPath();

            WeakReferenceMessenger.Default.Register<EmulatorMainMessage>(this, async (recipient, message) =>
            {
                if (!String.IsNullOrWhiteSpace(message.SelectedEmulator))
                {
                    Log.Information($"Current Emulator: {message.SelectedEmulator}");
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

        #region Radio Button Command
        [RelayCommand]
        public async Task SelectBackupMode()
        {
            //Automatically
            Log.Information($"Selected backup mode: {MainDomain.EmulatorSavedataDTO.BackUpMode}");
            
            //PPSSPP
            Log.Information($"Selected backup mode: {MainDomain.EmulatorSavedataDTO.ID}");

            await MainDomain.MainModel.UpdateBackupModeAsync(NameFormatter.UnformatEmulatorName(MainDomain.EmulatorSavedataDTO.ID.Trim()), MainDomain.EmulatorSavedataDTO.BackUpMode);

        }

        private void PropertyChangedName()
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

        #region From Path Command
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

        #region To Path Command
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
