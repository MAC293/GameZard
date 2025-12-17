using CommunityToolkit.Mvvm.ComponentModel;
using GameZard.Context;
using GameZard.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Domain
{
    //Exposes EmulatorDTO, combobox list of emulators, and selected emulators collection
    public partial class EmulatorDomain : ObservableObject
    {

        private EmulatorDTO _Emulator;

        [ObservableProperty]
        private ObservableCollection<String> _Emulators;

        [ObservableProperty]
        private ObservableCollection<EmulatorDTO> _SelectedEmulators;

        public EmulatorDomain()
        {
            Emulator = new EmulatorDTO();
            Emulators = new ObservableCollection<String>();
            SelectedEmulators = new ObservableCollection<EmulatorDTO>();
        }

        public EmulatorDTO Emulator
        {
            get { return _Emulator; }
            set { _Emulator = value; }
        }

    }
}
