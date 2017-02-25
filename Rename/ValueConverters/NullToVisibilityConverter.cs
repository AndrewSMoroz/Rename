using System;
using System.Windows;
using System.Windows.Data;

namespace Rename.ValueConverters
{
	public class NullToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool target = false;

			if (value != null)
			{
				target = true;
			}

			if (parameter != null)
			{
				target = !target;
			}

			return target ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
