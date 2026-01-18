using GameZard.Context;
using GameZard.DTO;
using GameZard.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace GameZard.Models
{
    public class MainModel
    {
        private GameZardDbContext _Context;

        public MainModel()
        {
            Context = new GameZardDbContext();
        }

        public GameZardDbContext Context
        {
            get { return _Context; }
            set { _Context = value; }
        }

        //Returns the current selected emulator

        //public IObservable<EmulatorSavedataDTO> CurrentEmulatorStream(string selectedEmulator)
        public async Task<EmulatorSavedataDTO> CurrentEmulatorAsync(String selectedEmulator)
        {
            //Log.Information($"Current Emulator: {message.SelectedEmulator}");

            var currentEmulator = await Context.EmulatorSavedata.FirstOrDefaultAsync(savedata => savedata.Emulator.Trim() == selectedEmulator.Trim());

            if (currentEmulator != null)
            {
                return new EmulatorSavedataDTO
                {
                    ID = NameFormatter.FormatCurrentEmulatorName(currentEmulator.Id)
                    //Icon =  ImageConverter.BLOBToBitmap(await EmulatorIconAsync(NameFormatter.SimpleFormatCurrentEmulatorName(currentEmulator.Id))),
                    //BackUpMode = currentEmulator.BackupMode,
                    //FromPath = currentEmulator.FromPath,
                    //ToPath = currentEmulator.ToPath,
                    //LastSave = currentEmulator.LastSave
                };
            }

            return null;
        }

        //Return the icon of the selected emulator
        public async Task<Byte[]> EmulatorIconAsync(String selectedEmulator)
        {
            var emulator = await Context.Emulators.FirstOrDefaultAsync(emulator => emulator.Name.Trim() == selectedEmulator.Trim());

            if (emulator != null)
            {
                return emulator.Icon;
            }

            return Array.Empty<Byte>();
        }
    }
}
