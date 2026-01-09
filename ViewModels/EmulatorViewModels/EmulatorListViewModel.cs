using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameZard.Domain;
using GameZard.DTO;
using GameZard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.EmulatorViewModels;
using Serilog;

namespace GameZard.ViewModels.EmulatorViewModels
{
    public partial class EmulatorListViewModel
    {
        private ListDomain _ListDomain;
        private EmulatorSavedataDTO _EmulatorSavedataDTO;

        public EmulatorListViewModel()
        {
            ListDomain = new ListDomain();
            EmulatorSavedataDTO = new EmulatorSavedataDTO();

            WeakReferenceMessenger.Default.Register<EmulatorSelectedMessage>(this, (recipient, message) =>
            {
                if (message.Emulator != null)
                {
                    ListDomain.EmulatorDomain.Emulator = message.Emulator;
                    ListDomain.LoadEmulators();
                }
            });
        }

        public ListDomain ListDomain
        {
            get { return _ListDomain; }
            set { _ListDomain = value; }
        }
        public EmulatorSavedataDTO EmulatorSavedataDTO
        {
            get { return _EmulatorSavedataDTO; }
            set { _EmulatorSavedataDTO = value; }
        }

        //Log.Information($"SelectedEmulatorAsync(): {emulatorSelectedModel}");
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

        [RelayCommand]
        public void SelectedEmulator(EmulatorDTO value)
        {
            if (value is null) return;

            Log.Information($"EmulatorSavedataDTO: {value.Name}");
        }
    }
}
