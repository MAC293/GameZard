using CommunityToolkit.Mvvm.Messaging;
using GameZard.Domain;
using GameZard.DTO;
using GameZard.Models;
using GameZard.Services;
using GameZard.ViewModels.EmulatorViewModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.EmulatorViewModels
{
    public class EmulatorViewModel
    {
        private EmulatorListViewModel _ELVM;

        public EmulatorViewModel()
        {

        }

        public EmulatorListViewModel ELVM
        {
            get { return _ELVM; }
            set { _ELVM = value; }
        }

    }
}
