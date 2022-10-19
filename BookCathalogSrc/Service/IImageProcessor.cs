using System.Windows.Media.Imaging;

namespace BookCathalog.Service
{
    public interface IImageProcessor
    {
        public byte[] ImageToByte(string sourceFile);

        public BitmapImage ByteToImage(byte[] imageData);
    }
}
