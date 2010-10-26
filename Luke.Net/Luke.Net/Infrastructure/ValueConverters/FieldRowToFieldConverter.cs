using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using Luke.Net.Features.Overview;

namespace Luke.Net.Infrastructure.ValueConverters
{
    class FieldGridSelectedRowToFieldsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var grid = (DataGrid) value;
            var selectedFields = grid.SelectedItems.Cast<FieldInfo>();
            return selectedFields;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
