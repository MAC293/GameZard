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
using GameZard.Domain;

namespace GameZard.Models
{
    public class ListModel
    {
        private GameZardDbContext _Context;

        public ListModel()
        {
            Context = new GameZardDbContext();
        }

        public GameZardDbContext Context
        {
            get { return _Context; }
            set { _Context = value; }
        }

        //Return a collection of Emulators that has Is_Selected = true
        public ObservableCollection<EmulatorDTO> LoadListAtStart()
        {
            var emulators = Context.Emulators
                .Where(e => e.IsSelected == true)
                .Select(e => new EmulatorDTO
                {
                    Name = NameFormatter.FormatEmulatorName(e.Name),
                    Icon = ImageConverter.BLOBToBitmap(e.Icon)
                })
                .ToList();

            return new ObservableCollection<EmulatorDTO>(emulators);
        }

        public async Task UncheckEmulatorAsync(String name)
        {
            var emulator = await Context.Emulators.FirstOrDefaultAsync(emulator => emulator.Name.Trim() == name.Trim());

            emulator.IsSelected = false;

            await Context.SaveChangesAsync();
        }
    }
}
