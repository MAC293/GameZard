using CommunityToolkit.Mvvm.Input;
using GameZard.Context;
using GameZard.Domain;
using GameZard.DTO;
using GameZard.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.EmulatorViewModels
{
    public partial class EmulatorSelectorViewModel
    {
        private SelectorDomain _SelectorDomain;
        public event EventHandler<EmulatorDTO> OnEmulatorSelectedDTO;

        public EmulatorSelectorViewModel()
        {
            SelectorDomain = new SelectorDomain();
            PropertyChangedName();
        }

        public SelectorDomain SelectorDomain
        {
            get { return _SelectorDomain; }
            set { _SelectorDomain = value; }
        }

        public ObservableCollection<String> FormattedEmulators()
        {
            var emulators = SelectorDomain.EmulatorDTO.Emulators;

            return NameFormatter.FormatEmulatorNames(emulators);

        }

        [RelayCommand(CanExecute = nameof(CanAddEmulator))]
        public async Task AddEmulator()
        {
            String selectedEmulator = SelectorDomain.EmulatorDTO.Name.Trim();
            String unformattedEmulator = NameFormatter.UnformatEmulatorName(selectedEmulator);

            //Change emulator selected property to true in the database
            await SelectorDomain.SelectorModel.SelectEmulatorAsync(unformattedEmulator);

            //Return the name and icon from the selected emulator
            var emulatorSelectedModel = await SelectorDomain.SelectorModel.SelectedEmulatorAsync(unformattedEmulator);

            Log.Information($"SelectedEmulatorAsync(): {emulatorSelectedModel}");

            if (emulatorSelectedModel != null)
            {
                //!: null-forgiving operator to ensure that OnEmulatorSelectedDTO is not null before invoking it. The value must be 100% not null
                //Send the selected emulator to the event handler thus to the EmulatorListViewModel
                OnEmulatorSelectedDTO?.Invoke(this, emulatorSelectedModel!);

                Log.Information($"EmulatorSelectorViewModel instance hash: {GetHashCode()}");

                Log.Information($"emulatorSelectedModel on Invoke: {emulatorSelectedModel.Name} {emulatorSelectedModel.Icon}");
            }
        }

        private Boolean CanAddEmulator()
        {
            if (SelectorDomain.EmulatorDTO.Name != "Select emulator")
            {
                return true;
            }

            return false;
        }

        private void PropertyChangedName()
        {
            SelectorDomain.EmulatorDTO.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectorDomain.EmulatorDTO.Name))
                {
                    AddEmulatorCommand.NotifyCanExecuteChanged();
                }
            };
        }

    }
}
