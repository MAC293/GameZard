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
            _ = StartAsync();
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
            await Watcher.WatchersCreation(await MainModel.AutomaticSavedataAsync());
        }
    }
}
