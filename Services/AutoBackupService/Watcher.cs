using GameZard.Context;
using GameZard.DTO;
using GameZard.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Services.AutoBackupService
{ 
    public class Watcher : IDisposable
    {
        private List<FileSystemWatcher> _Watchers;
        private readonly Debounce _Debounce = new();
        private readonly MainModel MainModel = new();

        public Watcher()
        {
            Watchers = new List<FileSystemWatcher>();
        }

        public List<FileSystemWatcher> Watchers
        {
            get { return _Watchers; }
            set { _Watchers = value; }
        }

        public async Task WatchersCreation(List<EmulatorSavedataDTO> emulators)
        {
            foreach (var emulator in emulators)
            {
                if (String.IsNullOrWhiteSpace(emulator.FromPath) ||
                    String.IsNullOrWhiteSpace(emulator.ToPath))

                    continue;

                var watcher = new FileSystemWatcher(emulator.FromPath)
                {
                    IncludeSubdirectories = true,
                    NotifyFilter = NotifyFilters.LastWrite
                                   | NotifyFilters.FileName
                                   | NotifyFilters.DirectoryName
                };

                watcher.Changed += (s, e) => DebounceEvent(emulator, e);
                watcher.Created += (s, e) => DebounceEvent(emulator, e);
                watcher.Renamed += (s, e) => DebounceEvent(emulator, e);
                watcher.Deleted += (s, e) => DebounceEvent(emulator, e);

                watcher.EnableRaisingEvents = true;
                Watchers.Add(watcher);
            }
        }

        private void DebounceEvent(EmulatorSavedataDTO emulator, FileSystemEventArgs e)
        {
            _Debounce.Execute(emulator.ID, 2000, () => OnFileChanged(emulator, e));
        }

        private async void OnFileChanged(EmulatorSavedataDTO emulator, FileSystemEventArgs e)
        {
            try
            {
                //Check target folder and permissions
                if (BackupEngine.TargetFolderExists(emulator.ToPath) &&
                    BackupEngine.HasWritePermission(emulator.ToPath))
                {
                    //Perform backup
                    await BackupEngine.BackupNowAsync(emulator.FromPath, emulator.ToPath);

                    //Update Last Save from Automatic backup
                    //ID from EmulatorSavedata
                    String ID = await MainModel.EmulatorIDByPathsAsync(emulator.FromPath, emulator.ToPath);
                    //Last time backup
                    String lastSave = BackupEngine.LastSaveTimeDate(DateTime.Now);
                    //Create a new save time on backup
                    _ = MainModel.UpdateLastSaveAsync(ID, lastSave);


                    Log.Information($"Backup completed for emulator {emulator.ID} at {e.FullPath}");
                }
                else
                {
                    Log.Warning($"Backup skipped for emulator {emulator.ID}: target path invalid or no permission.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error during backup for emulator {emulator.ID}");
            }
        }

        //Due to IDisposable implementation, resources are cleaned up when the FileSystemWatcher instances are no longer needed, preventing memory leaks and ensuring efficient resource management
        public void Dispose()
        {
            foreach (var watcher in Watchers)
                watcher.Dispose();

            Watchers.Clear();
            _Debounce.Dispose();
        }
    }
}
