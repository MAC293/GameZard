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
    public partial class SelectorListDomain : ObservableObject
    {
        private EmulatorDTO _Emulator;

        [ObservableProperty]
        private ObservableCollection<String> _Emulators;

        [ObservableProperty]
        private ObservableCollection<EmulatorDTO> _SelectedEmulators;

        public SelectorListDomain()
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
