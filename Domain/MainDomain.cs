using CommunityToolkit.Mvvm.ComponentModel;
using GameZard.DTO;
using GameZard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Domain
{
    public partial class MainDomain : ObservableObject
    {

        private MainModel _MainModel;
        private EmulatorSavedataDTO _EmulatorSavedataDTO;

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

        //public async Task DisplayEmulatorSavedataAsync(String selectedEmulator)
        //{
        //    var savedata = await MainModel.CurrentEmulatorAsync(selectedEmulator);

        //    if (savedata != null)
        //    {
        //        EmulatorSavedataDTO = savedata;
        //    }
        //}

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

    }
}
