using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.DTO;
using GameZard.Models;

namespace GameZard.Domain
{
    public class MainDomain
    {
        private MainModel _MainModel;
        private EmulatorSavedataDTO _EmulatorSavedataDTO;

        public MainDomain()
        {
            MainModel = new MainModel();
            EmulatorSavedataDTO = new EmulatorSavedataDTO();
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

        public async Task DisplayEmulatorSavedataAsync(String selectedEmulator)
        {
            var savedata = await MainModel.CurrentEmulatorAsync(selectedEmulator);

            if (savedata != null)
            {
                EmulatorSavedataDTO = savedata;
            }
        }
    }
}
