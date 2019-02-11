using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace TaskDoTo.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = System.Convert.ToInt32(value);

            if(status == 0)
            {
                return new SolidColorBrush( Color.FromRgb(244, 67, 54) );
            }
            else
            {
                return new SolidColorBrush( Color.FromRgb(118, 255, 3) );
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
