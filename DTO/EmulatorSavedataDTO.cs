using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.DTO
{
    public partial class EmulatorSavedataDTO : ObservableObject
    {
        [ObservableProperty]
        private String? _ID;

        [ObservableProperty]
        private Bitmap? _Icon;

        //[ObservableProperty]
        //private String _SelectedEmulator;

        [ObservableProperty]
        private String? _BackUpMode;

        [ObservableProperty]
        private String? _FromPath;

        [ObservableProperty]
        private String? _ToPath;

        [ObservableProperty]
        private String? _LastSave;

        //[ObservableProperty]
        //private String _Emulator;

    }
}

