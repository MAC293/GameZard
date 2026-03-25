using GameZard.DTO;
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
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName
                };

                watcher.Changed += (s, e) => OnFileChanged(emulator, e);
                watcher.Created += (s, e) => OnFileChanged(emulator, e);
                watcher.Renamed += (s, e) => OnFileChanged(emulator, e);

                watcher.EnableRaisingEvents = true;
                Watchers.Add(watcher);
            }
        }

        public event Action<EmulatorSavedataDTO, FileSystemEventArgs>? FileChanged;

        private void OnFileChanged(EmulatorSavedataDTO emulator, FileSystemEventArgs e)
        {
            FileChanged?.Invoke(emulator, e);
        }

        private void DebounceEvent(EmulatorSavedataDTO emulator, FileSystemEventArgs e)
        {
            _Debounce.Execute(emulator.ID, 2000, () => OnFileChanged(emulator, e));
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
