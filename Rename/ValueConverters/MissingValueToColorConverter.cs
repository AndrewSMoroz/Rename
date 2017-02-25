using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Rename.ValueConverters
{

    public class MissingValueToColorConverter : IValueConverter
    {

        //--------------------------------------------------------------------------------------------------------------
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string input = value as string;
            if (string.IsNullOrEmpty(input))
            {
                return Brushes.Yellow;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }

        }

        //--------------------------------------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
