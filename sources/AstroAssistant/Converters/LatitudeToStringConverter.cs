using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AstroAssistant.Converters
{
    public class LatitudeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Latitude)
            {
                return value.ToString();
            }
            else
            {
                return new Latitude().ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value == null) return new Latitude();
            //return Latitude.Parse(value.ToString());
            if (value == null) return System.Windows.DependencyProperty.UnsetValue;
            Latitude lat;
            if (Latitude.TryParse(value.ToString(), out lat))
                return lat;
            return System.Windows.DependencyProperty.UnsetValue; ;
        }
    }
}
