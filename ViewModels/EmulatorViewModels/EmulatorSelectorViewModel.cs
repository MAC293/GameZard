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
            //Use the same EmulatorDomain instance that SelectorDomain provides
            //EmulatorDomain = new EmulatorDomain();
            PropertyChangedName();
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

        //Execute
        [RelayCommand(CanExecute = nameof(CanAddEmulator))]
        public async Task AddEmulator()
        {
            String selectedEmulator = SelectorDomain.EmulatorDomain.Emulator.Name.Trim();
            String unformattedEmulator = NameFormatter.UnformatEmulatorName(selectedEmulator);

            await SelectorDomain.SelectorModel.SelectEmulatorAsync(unformattedEmulator);

            var emulatorSelectedModel = await SelectorDomain.SelectorModel.SelectedEmulatorAsync(unformattedEmulator);

            //Log.Information($"SelectedEmulatorAsync(): {emulatorSelectedModel}");

            if (emulatorSelectedModel != null)
            {

                WeakReferenceMessenger.Default.Send(new EmulatorSelectedMessage(emulatorSelectedModel));
            }
        }

        //CanExecute
        private Boolean CanAddEmulator()
        {
            if (SelectorDomain.EmulatorDomain.Emulator.Name != "Select emulator")
            {
                if (IsAvailable())
                {
                    return false;
                }
            }

            return true;
        }

        //Log.Information("SelectedEmulators != null");
        //Console.WriteLine("");
        private async Task<Boolean> IsAvailable()
        {
            List<String> selectedEmulatorNames = await SelectorDomain.SelectorModel.SelectedEmulatorNamesAsync();

            if (selectedEmulatorNames.Count == 0)
            {
                return true;
            }

            String selectedEmulatorName = SelectorDomain.EmulatorDomain.Emulator.Name.Trim();

            foreach (var emulatorName in selectedEmulatorNames)
            {
                if (emulatorName.Trim() != selectedEmulatorName.Trim())
                {
                    
                    return true;
                }
            }

            return false;
        }

        private void PropertyChangedName()
        {
            SelectorDomain.EmulatorDomain.Emulator.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectorDomain.EmulatorDomain.Emulator.Name))
                {
                    AddEmulatorCommand.NotifyCanExecuteChanged();
                }
            };
        }

    }
}
