using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;

namespace MockData.WookieBooks
{
    internal class ImageToByteArray
    {
        private readonly string _basePath;

        public ImageToByteArray()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            _basePath = Path.GetDirectoryName(path);
        }

        public byte[] GetImageBytes(string imageName)
        {
            using (var memoryStream = new MemoryStream())
            {
                var imagePath = Path.Combine(_basePath, "CoverImages", imageName);
                var image = new Bitmap(imagePath);

                image.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }
    }
}
