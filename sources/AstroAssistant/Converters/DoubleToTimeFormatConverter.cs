using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AstroAssistant.Converters
{
    /// <summary>
    /// Convert double to Time/Hour format
    /// </summary>
    public class DoubleToTimeFormatConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double val = System.Convert.ToDouble(value);
            if (parameter != null && parameter.ToString() == "hour")
                return Astro.AstroEngine.FormatToHour(val);
            else
                return Astro.AstroEngine.FormatToTime(val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
