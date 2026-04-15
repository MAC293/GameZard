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

        #region Read Emulator
        //Returns the current selected emulator
        public async Task<EmulatorSavedataDTO> CurrentEmulatorAsync(String selectedEmulator)
        {
            //Log.Information($"Current Emulator: {message.SelectedEmulator}");

            var currentEmulator = await Context.EmulatorSavedata.FirstOrDefaultAsync(savedata => savedata.Emulator.Trim() == selectedEmulator.Trim());

            if (currentEmulator != null)
            {
                return new EmulatorSavedataDTO
                {
                    ID = NameFormatter.FormatCurrentEmulatorName(currentEmulator.Id),
                    Icon = ImageConverter.BLOBToBitmap(await EmulatorIconAsync(NameFormatter.SimpleFormatCurrentEmulatorName(currentEmulator.Id))),
                    BackUpMode = currentEmulator.BackupMode,
                    FromPath = currentEmulator.FromPath,
                    ToPath = currentEmulator.ToPath,
                    LastSave = currentEmulator.LastSave
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
        #endregion

        #region Update Emulator
        //Update the backup mode of the selected emulator
        public async Task UpdateBackupModeAsync(String selectedEmulator, String backupMode)
        {
            var emulatorSavedata = await Context.EmulatorSavedata.FirstOrDefaultAsync(savedata => savedata.Emulator.Trim() == selectedEmulator.Trim());

            if (emulatorSavedata != null)
            {
                emulatorSavedata.BackupMode = backupMode;
                await Context.SaveChangesAsync();

            }
        }

        //Update the FromPath of the selected emulator     
        public async Task UpdateFromPathAsync(String selectedEmulator, String fromPath)
        {
            var emulatorSavedata = await Context.EmulatorSavedata.FirstOrDefaultAsync(savedata => savedata.Emulator.Trim() == selectedEmulator.Trim());

            if (emulatorSavedata != null)
            {
                emulatorSavedata.FromPath = fromPath;
                await Context.SaveChangesAsync();
            }
        }

        //Update the ToPath of the selected emulator     
        public async Task UpdateToPathAsync(String selectedEmulator, String toPath)
        {
            var emulatorSavedata = await Context.EmulatorSavedata.FirstOrDefaultAsync(savedata => savedata.Emulator.Trim() == selectedEmulator.Trim());

            if (emulatorSavedata != null)
            {
                emulatorSavedata.ToPath = toPath;
                await Context.SaveChangesAsync();
            }
        }

        //Update the LastSave of the selected emulator
        public async Task UpdateLastSaveAsync(String currentEmulator, String lastSaveTimeDate)
        {
            var emulatorSavedata = await Context.EmulatorSavedata.FirstOrDefaultAsync(savedata => savedata.Id.Trim() == currentEmulator.Trim());

            if (emulatorSavedata != null)
            {
                emulatorSavedata.LastSave = lastSaveTimeDate;
                await Context.SaveChangesAsync();
            }
        }
        #endregion

        #region First Load EmulatorSavedata
        //Return ID from the first emulator where its Is_Selected is true
        public async Task<String> SelectedEmulatorIDAsync()
        {
            var selectedEmulator = await Context.Emulators.FirstOrDefaultAsync(savedata => savedata.IsSelected == true);

            if (selectedEmulator != null)
            {
                return selectedEmulator.Name;
            }

            return String.Empty;
        }


        //Return the first emulator savedata from the list at startup
        public async Task<EmulatorSavedataDTO> LoadEmulatorsAsync(String emulatorID)
        {
            var currentEmulator = await Context.EmulatorSavedata.FirstOrDefaultAsync(savedata => savedata.Id.Trim() == emulatorID.Trim());

            if (currentEmulator != null)
            {
                return new EmulatorSavedataDTO
                {
                    ID = NameFormatter.FormatCurrentEmulatorName(currentEmulator.Id),
                    Icon = ImageConverter.BLOBToBitmap(await EmulatorIconAsync(NameFormatter.SimpleFormatCurrentEmulatorName(currentEmulator.Id))),
                    BackUpMode = currentEmulator.BackupMode,
                    FromPath = currentEmulator.FromPath,
                    ToPath = currentEmulator.ToPath,
                    LastSave = currentEmulator.LastSave
                };
            }

            return null;
        }
        #endregion

        #region Others
        //Return all Emulatorsavedata (ID, backup mode, from, to, last save) where backup mode is automatically 
        public async Task<List<EmulatorSavedataDTO>> AutomaticSavedataAsync()
        {
            var autoBackupEmulators = await Context.EmulatorSavedata.Where(savedata => savedata.BackupMode.Trim() == "Automatically").ToListAsync();

            if (autoBackupEmulators != null)
            {
                return autoBackupEmulators.Select(emulator => new EmulatorSavedataDTO
                {
                    ID = NameFormatter.FormatCurrentEmulatorName(emulator.Id),
                    FromPath = emulator.FromPath,
                    ToPath = emulator.ToPath,
                    LastSave = emulator.LastSave
                }).ToList();
            }

            return new List<EmulatorSavedataDTO>();
        }

        //Return ID of the EmulatorSavedata matching the FromPath/ToPath
        public async Task<String> EmulatorIDByPathsAsync(String fromPath, String toPath)
        {
            String normalizedFrom = fromPath?.Trim();
            String normalizedTo = toPath?.Trim();

            var emulatorSavedata = await Context.EmulatorSavedata
                .FirstOrDefaultAsync(savedata => savedata.FromPath == normalizedFrom && savedata.ToPath == normalizedTo);

            return emulatorSavedata?.Id ?? String.Empty;
        }
        #endregion
    }
}
