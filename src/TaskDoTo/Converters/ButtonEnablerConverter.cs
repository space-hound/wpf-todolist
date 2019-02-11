using System;
using System.Linq;
using System.Windows.Data;

namespace TaskDoTo.Converters
{
    public class ButtonEnablerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() < 2)
            {
                return false;
            }

            return (((string)values[0]).Length > 0 && ((string)values[1]).Length > 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
