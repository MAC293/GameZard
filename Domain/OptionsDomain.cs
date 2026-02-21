using CommunityToolkit.Mvvm.ComponentModel;
using GameZard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Domain
{
    public partial class OptionsDomain : ObservableObject
    {
        private OptionsModel _OptionsModel;

        public OptionsDomain()
        {
            OptionsModel = new OptionsModel();
        }

        public OptionsModel OptionsModel
        {
            get { return _OptionsModel; }
            set { _OptionsModel = value; }
        }

        //Return ToPath from the current selected emulator
        public async Task<String> TargetPathAsync(String selectedEmulator)
        {
            return await OptionsModel?.EmulatorTargetPathAsync(selectedEmulator);
        }

    }
}
