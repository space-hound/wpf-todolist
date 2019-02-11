using System;
using System.Windows.Data;
using System.Globalization;

namespace TaskDoTo.Converters
{
    public class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double screenSize = System.Convert.ToDouble(value);
            double percent = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);

            return ((int)(screenSize * percent)).ToString("G0", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
