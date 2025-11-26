using GameZard.ViewModels.EmulatorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.EmulatorViewModels;

namespace GameZard.Services
{
    public static class ViewModelLocator
    {
        public static EmulatorSelectorViewModel SelectorVM { get; } = new EmulatorSelectorViewModel();
        public static EmulatorListViewModel ListVM { get; } = new EmulatorListViewModel();
        //public static EmulatorViewModel EmulatorVM { get; } = new EmulatorViewModel(SelectorVM, ListVM);
    }
}
