using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using GameZard.Domain;
using GameZard.DTO;
using GameZard.Services;
using Serilog;


namespace GameZard.ViewModels.EmulatorViewModels
{
    public class EmulatorMainViewModel
    {
        private MainDomain _MainDomain;

        public EmulatorMainViewModel()
        {
            MainDomain = new MainDomain();

            WeakReferenceMessenger.Default.Register<EmulatorMainMessage>(this, async (recipient, message) =>
            {
                if (!String.IsNullOrWhiteSpace(message.SelectedEmulator))
                {
                    Log.Information($"Current Emulator: {message.SelectedEmulator}");
                    await LoadEmulatorSavedataAsync(NameFormatter.UnformatEmulatorName(message.SelectedEmulator));
                }
            });
        }

        public MainDomain MainDomain
        {
            get { return _MainDomain; }
            set { _MainDomain = value; }
        }

        public async Task LoadEmulatorSavedataAsync(String selectedEmulator)
        {
            await MainDomain.DisplayEmulatorSavedataAsync(selectedEmulator);
        }

    }
}
