using GameZard.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.DTO;
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
            var currentEmulator = await Context.EmulatorSavedata.FirstOrDefaultAsync(emulator => emulator.Id.Trim() == selectedEmulator);

            if (currentEmulator != null)
            {
                return new EmulatorSavedataDTO
                {
                    Id = currentEmulator.Id,
                    BackupMode = currentEmulator.BackupMode,
                    FromPath = currentEmulator.FromPath,
                    ToPath = currentEmulator.ToPath,
                    LastSave = currentEmulator.LastSave,
                    Emulator = currentEmulator.Emulator
                    //ID
                    //Icon
                    //SelectedEmulator
                    //BackUpMode
                    //FromPath
                    //ToPath
                    //LastSave
                    //Emulator
                };
            }
            else
            {
                return null;
            }
        }

        //Return the icon of the selected emulator
        public async Task<Byte[]> GetEmulatorIconAsync(String selectedEmulator)
        {
            var emulator = await Context.Emulators.FirstOrDefaultAsync(emulator => emulator.Name.Trim() == selectedEmulator.Trim());

            return emulator.Icon;
        }
    }
}
