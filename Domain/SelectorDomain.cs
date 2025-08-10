using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.DTO;
using GameZard.Models;

namespace GameZard.Domain
{
    public class SelectorDomain
    {
        private EmulatorDTO _EmulatorDTO;
        private SelectorModel _SelectorModel;

        public SelectorDomain()
        {
            EmulatorDTO = new EmulatorDTO();
            SelectorModel = new SelectorModel();
        }

        public EmulatorDTO EmulatorDTO
        {
            get { return _EmulatorDTO; }
            set { _EmulatorDTO = value; }
        }

        public SelectorModel SelectorModel
        {
            get { return _SelectorModel; }
            set { _SelectorModel = value; }
        }

        public void LoadEmulators()
        {
            EmulatorDTO.Emulators = SelectorModel.EmulatorNames();
        }

        public void EmulatorsPlaceholder()
        {
            var emulators = EmulatorDTO.Emulators;
            if (!emulators.Contains("Select emulator"))
            {
                emulators.Insert(0, "Select emulator");
            }

            EmulatorDTO.Name = "Select emulator";

            EmulatorDTO.Emulators = emulators;
        }
    }
}
