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
    public class AutoBackup
    {
        private MainModel _MainModel;
        private Watcher _Watcher;

        public AutoBackup()
        {
            MainModel = new MainModel();
            Watcher = new Watcher();
            //Log.Information($"Selected Emulator: {dto.Name}");
        }

        public MainModel MainModel
        {
            get { return _MainModel; }
            set { _MainModel = value; }
        }
        
        public Watcher Watcher
        {
            get { return _Watcher; }
            set { _Watcher = value; }
        }

        public async Task StartAsync()
        {
            Log.Information("AutoBackup service starting...");

            //Query emulators from database
            var emulators = await MainModel.AutomaticSavedataAsync();

            //Watchers creation for each emulator savedata from-to path
            await Watcher.WatchersCreation(emulators);
        }

        public void Stop()
        {
            Watcher.Dispose();
        }
    }
}
