using System;
using System.Windows;
using System.Windows.Data;

namespace Rename.ValueConverters
{
	/// <summary>
	/// Receives a 'is read-only' bool as a parameter.
	/// Returns 'Visibility.Collapsed' if read-only is true and 'Visibility.Visible' otherwise.
	/// </summary>
	public class ReadOnlyBoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool isReadOnly = (bool)value;

			return isReadOnly ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
