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
            var formatted = new ObservableCollection<String>();

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

        //Bring emulator name to its original format
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

        //Remove underscores from emulator name, and format specific name (Dolphin's)
        public static String FormatEmulatorName(String selectedEmulator)
        {
            String nintendoFormatted = String.Empty;

            if (selectedEmulator.Trim() == "Dolphin_Wii")
            {
                nintendoFormatted = "Dolphin (Wii)";
            }
            else if (selectedEmulator.Trim() == "Dolphin_GameCube")
            {
                nintendoFormatted = "Dolphin (GameCube)";
            }
            else if (selectedEmulator.Trim() != "Dolphin (Wii)" || selectedEmulator != "Dolphin (GameCube)")
            {
                String otherFormatted = selectedEmulator.Replace("_", " ").Trim();

                return otherFormatted;
            }

            return nintendoFormatted;
        }

    }
}
