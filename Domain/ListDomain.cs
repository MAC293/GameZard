using GameZard.DTO;
using GameZard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Domain
{
    public class ListDomain
    {
        private EmulatorDTO _EmulatorDTO;
        private SelectorModel _SelectorModel;

        public ListDomain()
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
            EmulatorDTO.SelectedEmulators.Add(EmulatorDTO);
        }
    }
}
