using System;
using System.Globalization;
using System.Windows.Data;

namespace BookCathalog.Convertors
{
    public class ByteArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            if (value is byte[] casted)
            {
                return (casted.Length != 0);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
