// Converters/RowNumberConverter.cs
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ViktorynaApp.Converters
{
    public class RowNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DataGridRow row && row.DataContext != null)
            {
                var dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;
                if (dataGrid != null)
                {
                    int index = dataGrid.Items.IndexOf(row.DataContext);
                    return (index + 1).ToString();
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}