using GameZard.Domain;
using GameZard.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using GameZard.DTO;

namespace ViewModels.EmulatorViewModels
{
    public partial class EmulatorSelectorViewModel
    {
        private SelectorDomain _SelectorDomain;
        public event EventHandler<EmulatorDTO> EmulatorSelected;

        public EmulatorSelectorViewModel()
        {
            SelectorDomain = new SelectorDomain();
            PropertyChangedNamee();
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

            //Send the selected emulator to the event handler thus to the EmulatorListViewModel
            EmulatorSelected.Invoke(this, emulatorSelectedModel);
        }

        private Boolean CanAddEmulator()
        {
            if (SelectorDomain.EmulatorDTO.Name != "Select emulator")
            {
                return true;
            }

            return false;
        }

        private void PropertyChangedNamee()
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
