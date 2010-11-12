using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Luke.Net.Infrastructure.ValueConverters
{
    class NegateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolVal = value as bool?;
            if (!boolVal.HasValue)
                return Binding.DoNothing;

            return !boolVal.Value;
            }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
