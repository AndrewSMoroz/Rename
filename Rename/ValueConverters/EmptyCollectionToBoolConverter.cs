using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Rename.ValueConverters
{

    public class EmptyCollectionToBoolConverter : IValueConverter
    {

        /// <summary>
        /// Value should be an IList.  If it is null or empty, returns false; otherwise returns true.  Pass any non-null parameter to achieve the inverse.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IList list = value as IList;
            bool bReturn = true;
            if (list == null || list.Count == 0) { bReturn = false; }
            if (parameter != null) { bReturn = !bReturn; }
            return bReturn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
