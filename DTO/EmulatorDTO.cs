using CommunityToolkit.Mvvm.ComponentModel;
using GameZard.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.DTO
{
    public partial class EmulatorDTO : ObservableObject
    {
        [ObservableProperty]
        private String _Name;

        [ObservableProperty]
        private Byte[] _Icon;

        [ObservableProperty]
        private String _Console;

        [ObservableProperty]
        private String _ExecutableLocation;

        [ObservableProperty]
        private ObservableCollection<String> _Emulators;

        [ObservableProperty]
        private ObservableCollection<EmulatorDTO> _SelectedEmulators;

        public EmulatorDTO()
        {
            Emulators = new ObservableCollection<String>();
            SelectedEmulators = new ObservableCollection<EmulatorDTO>();
        }
    }
}
