using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameZard.Services
{
    public static class BackupEngine
    {
        #region CanBackupNow
        //Check if target folder exists
        public static Boolean TargetFolderExists(String targetFolder)
        {
            Boolean exists = Directory.Exists(targetFolder);

            if (exists)
            {
                return true;
            }

            return false;
        }

        //Check program writing permissions on target folder
        public static Boolean HasWritePermission(String targetFolder)
        {
            try
            {
                var testFile = Path.Combine(targetFolder, Path.GetRandomFileName());

                using (File.Create(testFile)) { }

                File.Delete(testFile);

                return true; 
            }
            catch
            {
                return false;
            }
        }

        //TODO: "Check if target folder is not being used by another process" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running in gameplay.

        //TODO: "Check (MD5, SHA-256, etc) folder content corruption state". It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.
        #endregion

        #region BackupNow
        //TODO: "Overwrite the target folder without prompts using Delta" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.

        //Write on empty target folder
        public static async Task BackupToEmptyTarget(String fromPath, String toPath)
        {
           
        }
        #endregion

    }
}
