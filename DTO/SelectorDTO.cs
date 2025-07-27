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
    public partial class SelectorDTO : ObservableObject
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

        public SelectorDTO()
        {
            Emulators = new ObservableCollection<String>();
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public Byte[] Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        public String Console
        {
            get { return _Console; }
            set { _Console = value; }
        }

        public String ExecutableLocation
        {
            get { return _ExecutableLocation; }
            set { _ExecutableLocation = value; }
        }

        public ObservableCollection<String> Emulators
        {
            get { return _Emulators; }
            set { _Emulators = value; }
        }


    }
}
