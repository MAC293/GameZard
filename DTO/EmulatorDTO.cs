using CommunityToolkit.Mvvm.ComponentModel;
using GameZard.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace GameZard.DTO
{
    public partial class EmulatorDTO : ObservableObject
    {
        [ObservableProperty]
        private String _Name;

        [ObservableProperty]
        private Bitmap _Icon;

        [ObservableProperty]
        private String _Console;

        [ObservableProperty]
        private String _ExecutableLocation;

    }
}
