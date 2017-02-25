using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Rename.ValueConverters
{

    public class EmptyCollectionToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Value should be an IList.  If it is null or empty, returns Collapsed; otherwise returns Visible.  Pass any non-null parameter to achieve the inverse.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IList list = value as IList;
            bool bVisible = true;
            if (list == null || list.Count == 0) { bVisible = false; }
            if (parameter != null) { bVisible = !bVisible; }
            return bVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
