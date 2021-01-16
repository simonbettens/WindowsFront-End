using System;
using Windows.UI.Xaml.Data;

namespace WindowsFront_end.Converters
{
    public class DateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Retrieve the format string and use it to format the value.
            DateTime dt = (DateTime)value;
            return dt.ToString("dd/MM/yyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
