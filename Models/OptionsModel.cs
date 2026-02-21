using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZard.Context;
using Microsoft.EntityFrameworkCore;

namespace GameZard.Models
{
    public class OptionsModel
    {
        private GameZardDbContext _Context;

        public OptionsModel()
        {
            Context = new GameZardDbContext();
        }

        public GameZardDbContext Context
        {
            get { return _Context; }
            set { _Context = value; }
        }

        //Return the To_Path from EmulatorSaveData based on the current selected emulator
        public async Task<String?> EmulatorTargetPathAsync(String emulatorName)
        {
            var emulatorSaveData = await Context.EmulatorSavedata.FirstOrDefaultAsync(e => e.Emulator == emulatorName);

            if (String.IsNullOrEmpty(emulatorSaveData.ToPath))
            {
                emulatorSaveData.ToPath = String.Empty;
                return emulatorSaveData?.ToPath.Trim();
            }

            return emulatorSaveData?.ToPath.Trim();
        }
    }
}
