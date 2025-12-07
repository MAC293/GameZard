using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZard.Services
{
    public static class ImageConverter
    {
        public static Bitmap? BLOBToBitmap(Byte[]? blob)
        {
            if (blob == null || blob.Length == 0)

                return null;

            try
            {
                using var ms = new MemoryStream(blob);

                return new Bitmap(ms);
            }
            catch
            {
                return null;
            }
        }
    }
}
