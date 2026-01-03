using GameZard.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.DTO;
using GameZard.Services;
using Microsoft.EntityFrameworkCore;

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
        public async Task<EmulatorSavedataDTO> CurrentEmulatorAsync(String selectedEmulator)
        {

            //selectedEmulator comes as Save_PPSSPP converted from PPSSPP
            var currentEmulator = await Context.EmulatorSavedata.FirstOrDefaultAsync(emulator => emulator.Id.Trim() == selectedEmulator);

            if (currentEmulator != null)
            {
                return new EmulatorSavedataDTO
                {
                    ID = NameFormatter.FormatCurrentEmulatorName(currentEmulator.Id),
                    Icon =  ImageConverter.BLOBToBitmap(await EmulatorIconAsync(NameFormatter.SimpleFormatCurrentEmulatorName(currentEmulator.Id))),
                    BackUpMode = currentEmulator.BackupMode,
                    FromPath = currentEmulator.FromPath,
                    ToPath = currentEmulator.ToPath,
                    LastSave = currentEmulator.LastSave,
                    Emulator = currentEmulator.Emulator
                };
            }
            else
            {
                return null;
            }
        }

        //Return the icon of the selected emulator
        public async Task<Byte[]> EmulatorIconAsync(String selectedEmulator)
        {
            var emulator = await Context.Emulators.FirstOrDefaultAsync(emulator => emulator.Name.Trim() == selectedEmulator.Trim());

            return emulator.Icon;
        }
    }
}
