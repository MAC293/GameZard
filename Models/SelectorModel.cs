using GameZard.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameZard.Models
{
    public class SelectorModel
    {
        private GameZardDbContext _Context;

        public SelectorModel()
        {
            Context = new GameZardDbContext();
        }

        public GameZardDbContext Context
        {
            get { return _Context; }
            set { _Context = value; }
        }

        //Gets a collection of emulators with their icon-name
        //public ObservableCollection<SelectorDTO> Emulators()
        //{
        //    var emulatorsDAL = Context.Emulators.ToList();

        //    var selectorDTOs = new ObservableCollection<SelectorDTO>();

        //    foreach (var emulator in emulatorsDAL)
        //    {
        //        selectorDTOs.Add(new SelectorDTO()
        //        {
        //            Name = emulator.Name?.Trim(),
        //            Icon = emulator.Icon,
        //            Console = emulator.Console?.Trim(),
        //            ExecutableLocation = emulator.ExecutableLocation?.Trim() ?? String.Empty
        //        });
        //    }

        //    return selectorDTOs;
        //}

        //Return a collection of emulators name
        public ObservableCollection<String> EmulatorNames()
        {
            return new ObservableCollection<String>(Context.Emulators.Select(e => e.Name.Trim()));
        }

        //Change Emulator IsSelected property to true
        public async Task SelectEmulatorAsync(String emulatorName)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(emulatorName))
                {

                    var emulator = await Context.Emulators.Where(e => e.Name.Trim() == emulatorName.Trim()).FirstOrDefaultAsync();

                    if (emulator != null)
                    {
                        emulator.IsSelected = true;
                        await Context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in SelectEmulatorAsync: {ex}");
            }
        }
    }
}