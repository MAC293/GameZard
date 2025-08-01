using GameZard.Domain;
using GameZard.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace ViewModels.EmulatorViewModels
{
    public class EmulatorSelectorViewModel
    {
        private SelectorDomain _SelectorDomain;
        

        public EmulatorSelectorViewModel()
        {
            SelectorDomain = new SelectorDomain();
        }

        public SelectorDomain SelectorDomain
        {
            get { return _SelectorDomain; }
            set { _SelectorDomain = value; }
        }

        public ObservableCollection<String> FormattedEmulators()
        {
            var emulators = SelectorDomain.SelectorDTO.Emulators;

            return NameFormatter.FormatEmulatorNames(emulators);

        }
    }
}
