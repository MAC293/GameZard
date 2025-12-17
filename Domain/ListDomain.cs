using CommunityToolkit.Mvvm.ComponentModel;
using GameZard.Context;
using GameZard.DTO;
using GameZard.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Domain
{
    public partial class ListDomain : ObservableObject
    {
        private EmulatorDomain _EmulatorDomain;
        private ListModel _ListModel;

        public ListDomain()
        {
            EmulatorDomain = new EmulatorDomain();
            ListModel = new ListModel();
        }

        public EmulatorDomain EmulatorDomain
        {
            get { return _EmulatorDomain; }
            set { _EmulatorDomain = value; }
        }

        public ListModel ListModel
        {
            get { return _ListModel; }
            set { _ListModel = value; }
        }

        public void LoadEmulators()
        {
            EmulatorDomain.SelectedEmulators.Add(EmulatorDomain.Emulator);
        }

        public void LoadEmulatorsAtStart()
        {
            EmulatorDomain.SelectedEmulators = ListModel.LoadListAtStart();
        }
    }
}