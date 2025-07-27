using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Services
{
    public static class NameFormatter
    {
        //Remove underscores from emulators name, and format specific names (Dolphin's)
        public static ObservableCollection<String> FormatEmulatorNames(ObservableCollection<String> emulators)
        {
            var formatted = new ObservableCollection<string>();

            foreach (var emulator in emulators)
            {
                if (emulator == "Dolphin_Wii")
                {
                    formatted.Add("Dolphin (Wii)");
                }
                else if (emulator == "Dolphin_GameCube")
                {
                    formatted.Add("Dolphin (GameCube)");
                }
                else if (!string.IsNullOrEmpty(emulator))
                {
                    formatted.Add(emulator.Replace("_", " "));
                }
            }

            return formatted;
        }

        //Bring emulators list back to the original format
        public static String UnformatEmulatorName(String selectedEmulator)
        {
            String nintendoFormatted = String.Empty;

            if (selectedEmulator.Trim() == "Dolphin (Wii)")
            {
                nintendoFormatted = "Dolphin_Wii";
            }
            else if (selectedEmulator.Trim() == "Dolphin (GameCube)")
            {
                nintendoFormatted = "Dolphin_GameCube";
            }
            else if (selectedEmulator.Trim() != "Dolphin (Wii)" || selectedEmulator != "Dolphin (GameCube)")
            {
                String otherFormatted = selectedEmulator.Replace(" ", "_");

                return otherFormatted;
            }

            return nintendoFormatted;
        }

    }
}
