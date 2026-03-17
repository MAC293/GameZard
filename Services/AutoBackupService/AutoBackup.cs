using GameZard.Models;
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

        public AutoBackup()
        {
            MainModel = new MainModel();
        }

        public MainModel MainModel
        {
            get { return _MainModel; }
            set { _MainModel = value; }
        }

        public async Task StartAsync()
        {
            
        }
    }
}
