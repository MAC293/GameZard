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
        private SelectorListDomain _SelectorListDomain;
        private SelectorModel _SelectorModel;

        public SelectorDomain()
        {
            SelectorListDomain = new SelectorListDomain();
            SelectorModel = new SelectorModel();
        }

        public SelectorListDomain SelectorListDomain
        {
            get { return _SelectorListDomain; }
            set { _SelectorListDomain = value; }
        }

        public SelectorModel SelectorModel
        {
            get { return _SelectorModel; }
            set { _SelectorModel = value; }
        }

        public void LoadEmulators()
        {
            SelectorListDomain.Emulators = SelectorModel.EmulatorNames();
        }

        public void EmulatorsPlaceholder()
        {
            var emulators = SelectorListDomain.Emulators;

            if (!emulators.Contains("Select emulator"))
            {
                emulators.Insert(0, "Select emulator");
            }

            SelectorListDomain.Emulator.Name = "Select emulator";

            SelectorListDomain.Emulators = emulators;
        }
    }
}
