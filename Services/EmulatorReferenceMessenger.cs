using GameZard.DTO;
using GameZard.ViewModels.EmulatorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using ViewModels.EmulatorViewModels;

namespace GameZard.Services
{
    public record EmulatorSelectedMessage(EmulatorDTO Emulator);

    public record EmulatorMainMessage(String SelectedEmulator); 


}
