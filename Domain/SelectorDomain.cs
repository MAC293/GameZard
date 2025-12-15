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
        private EmulatorDomain _EmulatorDomain;
        private SelectorModel _SelectorModel;

        public SelectorDomain()
        {
            EmulatorDomain = new EmulatorDomain();
            SelectorModel = new SelectorModel();
        }

        public EmulatorDomain EmulatorDomain
        {
            get { return _EmulatorDomain; }
            set { _EmulatorDomain = value; }
        }

        public SelectorModel SelectorModel
        {
            get { return _SelectorModel; }
            set { _SelectorModel = value; }
        }

        public void LoadEmulators()
        {
            EmulatorDomain.Emulators = SelectorModel.EmulatorNames();
        }

        public void EmulatorsPlaceholder()
        {
            var emulators = EmulatorDomain.Emulators;

            if (!emulators.Contains("Select emulator"))
            {
                emulators.Insert(0, "Select emulator");
            }

            EmulatorDomain.Emulator.Name = "Select emulator";

            EmulatorDomain.Emulators = emulators;
        }
    }
}
