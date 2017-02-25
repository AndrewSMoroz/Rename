using System;
using System.Windows.Navigation;
using System.Windows.Data;

namespace Rename.ValueConverters
{

    public class BoolToNavigationUIVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool visible = (bool)value;
            return visible ? NavigationUIVisibility.Visible : NavigationUIVisibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
