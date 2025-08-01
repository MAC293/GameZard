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
        private SelectorDTO _SelectorDTO;
        private SelectorModel _SelectorModel;

        public SelectorDomain()
        {
            SelectorDTO = new SelectorDTO();
            SelectorModel = new SelectorModel();
        }

        public SelectorDTO SelectorDTO
        {
            get { return _SelectorDTO; }
            set { _SelectorDTO = value; }
        }

        public SelectorModel SelectorModel
        {
            get { return _SelectorModel; }
            set { _SelectorModel = value; }
        }

        public void LoadEmulators()
        {
            SelectorDTO.Emulators = SelectorModel.EmulatorNames();
        }

        //public void EmulatorsPlaceholder()
        //{
        //    var emulators = SelectorDTO.Emulators;

        //    emulators.Insert(0, "Select emulator");

        //    SelectorDTO.Emulators = emulators;
        //}
    }
}
