using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using WindowsFront_end.Models;

namespace WindowsFront_end.Converters
{
    class ItemTypeToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ItemType itemType = (ItemType) value;
            return itemType == ItemType.ToDo ? "to do" : "to pack";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string itemType = (string)value;
            return itemType == "TO DO" ? ItemType.ToDo : ItemType.ToPack;
        }
    }
}
