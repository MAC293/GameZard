using CommunityToolkit.Mvvm.ComponentModel;
using GameZard.DTO;
using GameZard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.Services;

namespace GameZard.Domain
{
    //Log.Information("SelectedEmulators != null");
    public partial class MainDomain : ObservableObject
    {
        private EmulatorSavedataDTO _EmulatorSavedataDTO;
        private MainModel _MainModel;

        public MainDomain()
        {
            EmulatorSavedataDTO = new EmulatorSavedataDTO();
            MainModel = new MainModel();
        }

        public MainModel MainModel
        {
            get { return _MainModel; }
            set { _MainModel = value; }
        }

        public EmulatorSavedataDTO EmulatorSavedataDTO
        {
            get { return _EmulatorSavedataDTO; }
            set { _EmulatorSavedataDTO = value; }
        }

        //Display selected emulator savedata
        public async Task DisplayEmulatorSavedataAsync(String selectedEmulator)
        {
            var savedata = await MainModel.CurrentEmulatorAsync(selectedEmulator);

            if (savedata == null)

                return;

            EmulatorSavedataDTO.ID = savedata.ID;
            EmulatorSavedataDTO.Icon = savedata.Icon;
            EmulatorSavedataDTO.BackUpMode = savedata.BackUpMode;
            EmulatorSavedataDTO.FromPath = savedata.FromPath;
            EmulatorSavedataDTO.ToPath = savedata.ToPath;
            EmulatorSavedataDTO.LastSave = savedata.LastSave;
        }

        //Display first emulator savedata at startup
        public async Task DisplayEmulatorSavedataStartAsync()
        {
            String emulatorIDRaw = await MainModel.SelectedEmulatorIDAsync();
            String emulatorIDFormatted =  NameFormatter.FormatEmulatorNameToSavedataID(emulatorIDRaw);

            var savedata = await MainModel.LoadEmulatorsAsync(emulatorIDFormatted);

            if (savedata == null)

                return;

            EmulatorSavedataDTO.ID = savedata.ID;
            EmulatorSavedataDTO.Icon = savedata.Icon;
            EmulatorSavedataDTO.BackUpMode = savedata.BackUpMode;
            EmulatorSavedataDTO.FromPath = savedata.FromPath;
            EmulatorSavedataDTO.ToPath = savedata.ToPath;
            EmulatorSavedataDTO.LastSave = savedata.LastSave;
        }

    }
}
