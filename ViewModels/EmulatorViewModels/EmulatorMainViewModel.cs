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
        public EmulatorMainViewModel()
        {
            WeakReferenceMessenger.Default.Register<EmulatorMainMessage>(this, (recipient, message) =>
            {
                if (!String.IsNullOrWhiteSpace(message.SelectedEmulator))
                {
                    Log.Information($"Current Emulator: {message.SelectedEmulator}");
                }
            });
        }
    }
}
