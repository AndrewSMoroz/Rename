using System;
using System.Windows;
using System.Windows.Data;

namespace Rename.ValueConverters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// If value == true, Visibility.Visible will be returned.
        /// If value == false and parameter == "hidden", Visibility.Hidden will be returned
        /// If value == false and parameter != "hidden" or is null, Visibility.Collapsed will be returned
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool visible = (bool)value;
            Visibility valueToReturnIfFalse = Visibility.Collapsed;
            if (parameter != null && parameter.ToString().ToLower().Equals("hidden")) { valueToReturnIfFalse = Visibility.Hidden; } 
            return visible ? Visibility.Visible : valueToReturnIfFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
