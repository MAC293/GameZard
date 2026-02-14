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
using Serilog;
using CommunityToolkit.Mvvm.Messaging;

namespace ViewModels.EmulatorViewModels
{
    public partial class EmulatorSelectorViewModel
    {
        private SelectorDomain _SelectorDomain;
        private Boolean _IsEmulatorAvailableCache;

        public EmulatorSelectorViewModel()
        {
            SelectorDomain = new SelectorDomain();
            PropertyChangedName();
            //Ignore its return value, and don’t await it.
            _ = IsAvailableAsync();
        }

        public SelectorDomain SelectorDomain
        {
            get { return _SelectorDomain; }
            set { _SelectorDomain = value; }
        }

        public Boolean IsEmulatorAvailableCache
        {
            get { return _IsEmulatorAvailableCache; }
            set { _IsEmulatorAvailableCache = value; }
        }

        public ObservableCollection<String> FormattedEmulators()
        {
            var emulators = SelectorDomain.EmulatorDomain.Emulators;

            return NameFormatter.FormatEmulatorNames(emulators);

        }


        [RelayCommand(CanExecute = nameof(CanAddEmulator))]
        public async Task AddEmulator()
        {
            String selectedEmulator = SelectorDomain.EmulatorDomain.Emulator.Name.Trim();
            String unformattedEmulator = NameFormatter.UnformatEmulatorName(selectedEmulator);

            await SelectorDomain.SelectorModel.SelectEmulatorAsync(unformattedEmulator);

            await IsAvailableAsync();

            var emulatorSelectedModel = await SelectorDomain.SelectorModel.SelectedEmulatorAsync(unformattedEmulator);

            if (emulatorSelectedModel != null)
            {

                WeakReferenceMessenger.Default.Send(new EmulatorSelectedMessage(emulatorSelectedModel));
            }
        }

        private Boolean CanAddEmulator()
        {
            if (SelectorDomain.EmulatorDomain.Emulator.Name != "Select emulator")
            {
                if (IsEmulatorAvailableCache)
                {
                    return true;
                }
            }

            return false;
        }

        //Log.Information("SelectedEmulators != null");
        //Console.WriteLine("");
        private async Task IsAvailableAsync()
        {
            List<String> selectedEmulatorNames = await SelectorDomain.SelectorModel.SelectedEmulatorNamesAsync();

            //SelectorDomain.EmulatorDomain.Emulator == null
            if (String.IsNullOrEmpty(SelectorDomain.EmulatorDomain.Emulator.Name))
            {
                IsEmulatorAvailableCache = false;
                return;
            }

            String selectedEmulatorName = SelectorDomain.EmulatorDomain.Emulator.Name.Trim();

            if (selectedEmulatorNames.Count == 0)
            {
                IsEmulatorAvailableCache = true;
                return;
            }

            Boolean isAvailable = true;

            foreach (var emulatorName in selectedEmulatorNames)
            {
                if (emulatorName.Trim() == selectedEmulatorName)
                {
                    isAvailable = false;
                    break;
                }
            }

            IsEmulatorAvailableCache = isAvailable;
        }

        private void PropertyChangedName()
        {
            SelectorDomain.EmulatorDomain.Emulator.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(SelectorDomain.EmulatorDomain.Emulator.Name))
                {
                    await IsAvailableAsync();
                    AddEmulatorCommand.NotifyCanExecuteChanged();
                }
            };
        }

    }
}
