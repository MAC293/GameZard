using GameZard.DTO;
using GameZard.Models;
using GameZard.ViewModels.EmulatorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.EmulatorViewModels
{
    public class EmulatorViewModel
    {
        private EmulatorListViewModel _ELVM; 
        private EmulatorSelectorViewModel _ESVM; 

        public EmulatorViewModel()
        {
            ELVM = new EmulatorListViewModel();
            ESVM = new EmulatorSelectorViewModel();
            ESVM.OnEmulatorSelectedDTO += OnEmulatorSelected;

        }

        public EmulatorListViewModel ELVM
        {
            get { return _ELVM; }
            set { _ELVM = value; }
        } 
        
        public EmulatorSelectorViewModel ESVM
        {
            get { return _ESVM; }
            set { _ESVM = value; }
        }

        private void OnEmulatorSelected(Object sender, EmulatorDTO dto)
        {
            ELVM.ListDomain.EmulatorDTO = dto;
        }
    }
}
