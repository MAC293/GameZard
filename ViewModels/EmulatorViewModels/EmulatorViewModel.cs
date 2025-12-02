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
        private EmulatorSelectorViewModel _ESVM; 

        public EmulatorViewModel()
        {
            //ESVM = new EmulatorSelectorViewModel();
            ELVM = new EmulatorListViewModel();
            //ESVM.OnEmulatorSelectedDTO += OnEmulatorSelected;

            //WeakReferenceMessenger.Default.Register<EmulatorSelectedMessage>(this, (recipient, message) =>
            //{

            //    ELVM.ListDomain.EmulatorDTO = message.Emulator;
            //    Log.Information($"message.Emulator on EmulatorViewModel: {message.Emulator}");  
            //    ELVM.ListDomain.LoadEmulators();
            //});

            WeakReferenceMessenger.Default.Register<EmulatorSelectedMessage>(this, (recipient, message) =>
            {
                Log.Information("Receiver: hash = {hash}, Name = {Name}, IconLen = {len}",
                    this.GetHashCode(), message.Emulator?.Name, message.Emulator.Icon?.Length);

                if (message.Emulator != null)
                {
                    ELVM.ListDomain.EmulatorDTO = message.Emulator;
                    ELVM.ListDomain.LoadEmulators();
                }
            });

        }

        public EmulatorListViewModel ELVM
        {
            get { return _ELVM; }
            set { _ELVM = value; }
        } 
        
        public EmulatorSelectorViewModel ESVM
        {
            get { return _ESVM; }
            set { _ESVM = value; }
        }

        //public void OnEmulatorSelected(Object sender, EmulatorDTO dto)
        //{
        //    ELVM.ListDomain.EmulatorDTO = dto;

        //    Log.Information($"dto on OnEmulatorSelected on EmulatorViewModel: {dto.Name} {dto.Icon}");
        //    ELVM.ListDomain.LoadEmulators();

        //    Log.Information($"EmulatorViewModel instance hash: {GetHashCode()}");

        //}

    }
}
