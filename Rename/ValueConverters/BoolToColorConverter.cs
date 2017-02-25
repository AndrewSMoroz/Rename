using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Rename.ValueConverters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool boolValue = (bool)value;
            Brush brush = null;

            if (boolValue)
            {
                brush = Brushes.LightGray;
            }
            else
            {
                brush = Brushes.Transparent;
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
