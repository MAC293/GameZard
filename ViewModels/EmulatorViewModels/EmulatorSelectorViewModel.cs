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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace ViewModels.EmulatorViewModels
{
    public partial class EmulatorSelectorViewModel
    {
        private SelectorDomain _SelectorDomain;
        private SelectorListDomain _SelectorListDomain;

        public EmulatorSelectorViewModel()
        {
            SelectorDomain = new SelectorDomain();
            SelectorListDomain = new SelectorListDomain();
            PropertyChangedName();
        }

        public SelectorListDomain SelectorListDomain
        {
            get { return _SelectorListDomain; }
            set { _SelectorListDomain = value; }
        }

        public SelectorDomain SelectorDomain
        {
            get { return _SelectorDomain; }
            set { _SelectorDomain = value; }
        }

        public ObservableCollection<String> FormattedEmulators()
        {
            var emulators = SelectorDomain.SelectorListDomain.Emulators;

            return NameFormatter.FormatEmulatorNames(emulators);

        }

        [RelayCommand(CanExecute = nameof(CanAddEmulator))]
        public async Task AddEmulator()
        {
            String selectedEmulator = SelectorDomain.SelectorListDomain.Emulator.Name.Trim();
            String unformattedEmulator = NameFormatter.UnformatEmulatorName(selectedEmulator);

            await SelectorDomain.SelectorModel.SelectEmulatorAsync(unformattedEmulator);

            var emulatorSelectedModel = await SelectorDomain.SelectorModel.SelectedEmulatorAsync(unformattedEmulator);

            //Log.Information($"SelectedEmulatorAsync(): {emulatorSelectedModel}");

            if (emulatorSelectedModel != null)
            {

                WeakReferenceMessenger.Default.Send(new EmulatorSelectedMessage(emulatorSelectedModel));
            }
        }

        private Boolean CanAddEmulator()
        {
            if (SelectorDomain.SelectorListDomain.Emulator.Name != "Select emulator")
            {
                //return true;
                if (IsAvailable())
                {
                    return true;
                }
            }

            return false;
        }

        private Boolean IsAvailable()
        {
            //Log.Information($"SelectorListDomain.SelectedEmulators.Count: {SelectorListDomain.SelectedEmulators.Count}");

            if (SelectorListDomain.SelectedEmulators.Count == 0)
            {
                return true;
            }
            
            if (SelectorListDomain.SelectedEmulators.Count >= 1)
            {
                foreach (var emulator in SelectorListDomain.SelectedEmulators)
                {
                    foreach (var selectedEmulator in SelectorListDomain.Emulators)
                    {
                        if (emulator.Name == selectedEmulator)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void PropertyChangedName()
        {
            SelectorDomain.SelectorListDomain.Emulator.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectorDomain.SelectorListDomain.Emulator.Name))
                {
                    AddEmulatorCommand.NotifyCanExecuteChanged();
                }
            };
        }

    }
}
