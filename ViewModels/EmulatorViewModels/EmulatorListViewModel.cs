using CommunityToolkit.Mvvm.Input;
using GameZard.Domain;
using GameZard.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.EmulatorViewModels;

namespace GameZard.ViewModels.EmulatorViewModels
{
    public partial class EmulatorListViewModel
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

        [RelayCommand]
        public async Task RemoveEmulator(EmulatorDTO dto)
        {
            var index = ListDomain.EmulatorDomain.SelectedEmulators.IndexOf(dto);

            if (index >= 0)
            {
                ListDomain.EmulatorDomain.SelectedEmulators.RemoveAt(index);
                await ListDomain.ListModel.UncheckEmulatorAsync(dto.Name);
            }
        }
    }
}
