using System.IO;
using System.Windows.Media.Imaging;

namespace BookCathalog.Service
{
    public class ImageProcessor : IImageProcessor
    {
        public byte[] ImageToByte(string sourceFile)
        {
            return File.ReadAllBytes(sourceFile);
        }

        public BitmapImage ByteToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
