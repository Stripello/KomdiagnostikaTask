using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookCathalog.Service
{
    public interface IImageProcessor
    {
        public byte[] ImageToByte(string sourceFile);

        public BitmapImage ByteToImage(byte[] imageData);
    }
}
