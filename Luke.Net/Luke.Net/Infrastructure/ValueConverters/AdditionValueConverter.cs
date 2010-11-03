using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Luke.Net.Infrastructure.ValueConverters
{
    class AdditionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input1 = System.Convert.ToDouble(value);
            var input2 = System.Convert.ToDouble(parameter);
            return input1 + input2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
