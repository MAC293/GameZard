using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GameZard.Context;
using Microsoft.EntityFrameworkCore;

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
        #endregion

        #region Backup Now
        //Write/Overwrite the target folder without prompts using Delta
        public static async Task BackupNowAsync(String sourcePath, String targetPath)
        {
            foreach (string sourceFilePath in Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                String relativePath = sourceFilePath.Substring(sourcePath.Length).TrimStart(Path.DirectorySeparatorChar);
                String targetFilePath = Path.Combine(targetPath, relativePath);

                String targetDirectory = Path.GetDirectoryName(targetFilePath);
                Directory.CreateDirectory(targetDirectory);

                if (File.Exists(targetFilePath))
                {
                    String sourceChecksum = await CalculateFileChecksumAsync(sourceFilePath);
                    String targetChecksum = await CalculateFileChecksumAsync(targetFilePath);

                    if (sourceChecksum != targetChecksum)
                    {
                        File.Copy(sourceFilePath, targetFilePath, overwrite: true);
                        //Console.WriteLine($"Updated: {relativePath}");
                    }
                }
                else
                {
                    File.Copy(sourceFilePath, targetFilePath);
                    //Console.WriteLine($"Copied: {relativePath}");
                }
            }

            //Optionally, delete files in the target that no longer exist in the source
            foreach (string targetFilePath in Directory.EnumerateFiles(targetPath, "*", SearchOption.AllDirectories))
            {
                String relativePath = targetFilePath.Substring(targetPath.Length).TrimStart(Path.DirectorySeparatorChar);
                String sourceFilePath = Path.Combine(sourcePath, relativePath);

                if (!File.Exists(sourceFilePath))
                {
                    File.Delete(targetFilePath);
                    //Console.WriteLine($"Deleted: {relativePath}");
                }
            }
        }

        //Generate and compare checksums to ensure to-path folders are identical
        //Check (MD5, SHA-256, etc) folder content corruption state
        private static async Task<String> CalculateFileChecksumAsync(String filePath)
        {
            using (var md5 = MD5.Create())

            using (var stream = File.OpenRead(filePath))
            {
                byte[] hashBytes = await md5.ComputeHashAsync(stream);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        //TODO: "Generate graceful failure if the files failed to copy. Log the error and carry on with the next file" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.

        //TODO: "Notifications: success, failure, warning" is paused for now. It'll be resumed later after all methods, and UIs are implemented, to avoid blocking the development process. It has to be running while a real backup data is created during gameplay.

        //Set 'Last Save' variable after copying process is done
        public static String LastSaveTimeDate(DateTime lastSaveTimeDate)
        {
            String date = lastSaveTimeDate.ToString("dd/MM/yyyy");
            String time = lastSaveTimeDate.ToString("HH:mm");

            return $"{date} at {time}";
        }
        #endregion

    }
}
