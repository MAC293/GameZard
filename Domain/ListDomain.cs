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
        private SelectorListDomain _SelectorListDomain;

        public ListDomain()
        {
            SelectorListDomain = new SelectorListDomain();
        }

        public SelectorListDomain SelectorListDomain
        {
            get { return _SelectorListDomain; }
            set { _SelectorListDomain = value; }
        }

        public void LoadEmulators()
        {
            SelectorListDomain.SelectedEmulators.Add(SelectorListDomain.Emulator);
        }
    }
}