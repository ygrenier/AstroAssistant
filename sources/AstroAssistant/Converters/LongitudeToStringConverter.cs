using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AstroAssistant.Converters
{
    public class LongitudeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Longitude)
            {
                return value.ToString();
            }
            else
            {
                return new Longitude().ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return new Longitude();
            return Longitude.Parse(value.ToString());
        }
    }
}
