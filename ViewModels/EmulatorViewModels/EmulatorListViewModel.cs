using GameZard.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.EmulatorViewModels;

namespace GameZard.ViewModels.EmulatorViewModels
{
    public class EmulatorListViewModel
    {
        private EmulatorDTO _EmulatorDisplay;

        public EmulatorListViewModel()
        {
            
        }

        public EmulatorDTO EmulatorDisplay
        {
            get { return _EmulatorDisplay; }
            set { _EmulatorDisplay = value; }
        }
    }
}
