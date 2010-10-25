using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using Luke.Net.Features.LuceneIndex;

namespace Luke.Net.ValueConverters
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
