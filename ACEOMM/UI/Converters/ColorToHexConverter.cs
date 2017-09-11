using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ACEOMM.UI.Converters
{
    public class ColorToHexConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var color = (Color)value;
            return color.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var hex = (string)value;
            try
            {
                return (Color)ColorConverter.ConvertFromString(hex);
            }
            catch
            {
                return Colors.White;
            }
        }
    }
}
