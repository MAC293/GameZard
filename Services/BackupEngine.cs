using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Services
{
    public static class BackupEngine
    {
        public static async Task CopyFolderAsync(String fromPath, String toPath)
        {
            await Task.Run(() =>
            {
                //Ensure that the source folder exists
                if (!System.IO.Directory.Exists(fromPath))
                {
                    throw new System.IO.DirectoryNotFoundException($"Source folder not found: {fromPath}");
                }

                //Create the destination folder if it doesn't exist
                if (!System.IO.Directory.Exists(toPath))
                {
                    System.IO.Directory.CreateDirectory(toPath);
                }

                //Get the files in the source folder and copy them to the destination folder
                foreach (var filePath in System.IO.Directory.GetFiles(fromPath))
                {
                    var fileName = System.IO.Path.GetFileName(filePath);
                    var destFilePath = System.IO.Path.Combine(toPath, fileName);
                    
                    System.IO.File.Copy(filePath, destFilePath, true);
                }

                //Recursively copy subfolders
                foreach (var directoryPath in System.IO.Directory.GetDirectories(fromPath))
                {
                    var directoryName = System.IO.Path.GetFileName(directoryPath);
                    var destDirectoryPath = System.IO.Path.Combine(toPath, directoryName);
                    CopyFolderAsync(directoryPath, destDirectoryPath).Wait();
                }
            });
        }
    }
}
