using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WindowsFront_end.Converters
{
    public class IntToVisibilityConverter : IValueConverter
    {
        public Visibility OnTrue { get; set; }
        public Visibility OnFalse { get; set; }

        public IntToVisibilityConverter()
        {
            OnFalse = Visibility.Collapsed;
            OnTrue = Visibility.Visible;
        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var v = (int)value;

            return v > 0 ? OnTrue : OnFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility == false)
                return DependencyProperty.UnsetValue;

            if ((Visibility)value == OnTrue)
                return true;
            else
                return false;
        }
    }

    public class IntToVisibilityConverterReverse : IValueConverter
    {
        public Visibility OnTrue { get; set; }
        public Visibility OnFalse { get; set; }

        public IntToVisibilityConverterReverse()
        {
            OnFalse = Visibility.Collapsed;
            OnTrue = Visibility.Visible;
        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var v = (int)value;

            return v == 0 ? OnTrue : OnFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility == false)
                return DependencyProperty.UnsetValue;

            if ((Visibility)value == OnTrue)
                return true;
            else
                return false;
        }
    }
}
