using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Rename.ValueConverters
{
    public class NullOrEmptyStringToBoolConverter : IValueConverter
    {
        // Should be applied to string types only
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool temp = false;

            if (value != null && value.GetType() == typeof(string) && !string.IsNullOrEmpty(value as string))
            {
                temp = true;
            }

            if (parameter != null)
            {
                temp = !temp;
            }

            return temp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
