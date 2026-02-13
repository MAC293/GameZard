using GameZard.Context;
using GameZard.DTO;
using GameZard.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;


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
        //public ObservableCollection<EmulatorDTO> Emulators()
        //{
        //    var emulatorsDAL = Context.Emulators.ToList();

        //    var emulatorsDTO = new ObservableCollection<EmulatorDTO>();

        //    foreach (var emulator in emulatorsDAL)
        //    {
        //        emulatorsDTO.Add(new EmulatorDTO()
        //        {
        //            Name = NameFormatter.FormatEmulatorName(emulator.Name.Trim()),
        //            Icon = ImageConverter.ToBitmap(emulator.Icon)
        //        });
        //    }

        //    return emulatorsDTO;
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

        //Return the selected emulator name and icon
        public async Task<EmulatorDTO> SelectedEmulatorAsync(String emulatorName)
        {
            try
            {
                var emulator = await Context.Emulators.FirstOrDefaultAsync(emulator => emulator.Name.Trim() == emulatorName);

                if (emulator != null)
                {

                    if (emulator.Icon != null && emulator.Icon.Length > 0)
                    {
                        var selectedEmulator = new EmulatorDTO()
                        {
                            Name = NameFormatter.FormatEmulatorName(emulator.Name),
                            //Icon = emulator.Icon
                            Icon = ImageConverter.BLOBToBitmap(emulator.Icon)

                        };

                        return selectedEmulator;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in GetSelectedEmulatorAsync: {ex}");
                return null;
            }
        }

        //Return a list of emulators name where IsSelected is true
        public async Task<List<String>> SelectedEmulatorNamesAsync()
        {
            return await Context.Emulators.Where(e => e.IsSelected == true).Select(e => e.Name.Trim()).ToListAsync();
        }
    }
}