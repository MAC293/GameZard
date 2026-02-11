using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //TODO: "Write/Overwrite the target folder without prompts using Delta" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.

        //TODO: "Generate and compare checksums to ensure to-path folders are identical" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.

        //TODO: "Generate graceful failure if the files failed to copy. Log the error and carry on with the next file" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.

        //TODO: "Notifications: success, failure, warning" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.

        //TODO: "Set 'Last Save' variable after copying process is done". It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.
        #endregion

    }
}
