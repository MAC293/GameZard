using GameZard.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.Domain;
using ViewModels.EmulatorViewModels;

namespace GameZard.ViewModels.EmulatorViewModels
{
    public class EmulatorListViewModel
    {
        private ListDomain _ListDomain;

        public EmulatorListViewModel()
        {
            ListDomain = new ListDomain();
        }

        public ListDomain ListDomain
        {
            get { return _ListDomain; }
            set { _ListDomain = value; }
        }
    }
}
