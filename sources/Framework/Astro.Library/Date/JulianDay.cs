using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{

    /// <summary>
    /// Represents a Julian Day in Universal Time
    /// </summary>
    public struct JulianDay
    {

        #region Constants

        /// <summary>
        /// 2000 January 1.5
        /// </summary>
        public const double J2000 = 2451545.0;

        /// <summary>
        /// 1950 January 0.923 
        /// </summary>
        public const double B1950 = 2433282.42345905;

        /// <summary>
        /// 1900 January 0.5
        /// </summary>
        public const double J1900 = 2415020.0;

        /// <summary>
        /// First Julian Day of the Gregorian calendar : October 15, 1582
        /// </summary>
        public const double GregorianFirstJD = 2299160.5;

        #endregion

        #region Helpers

        /// <summary>
        /// Get default calendar from a Julian Day
        /// </summary>
        /// <remarks>
        /// Gregorian calendar start at October 15, 1582
        /// </remarks>
        public static DateCalendar GetCalendar(double jd)
        {
            return jd >= GregorianFirstJD ? DateCalendar.Gregorian : DateCalendar.Julian;
        }

        /// <summary>
        /// Get default calendar from a date
        /// </summary>
        /// <remarks>
        /// Gregorian calendar start at October 15, 1582
        /// </remarks>
        public static DateCalendar GetCalendar(int year, int month, int day)
        {
            int date = (year * 10000) + (month * 100) + day;
            return date >= 15821115 ? DateCalendar.Gregorian : DateCalendar.Julian;
        }

        #endregion

        /// <summary>
        /// Create a new Julian Day from his value
        /// </summary>
        /// <param name="val">The Julian Day value</param>
        public JulianDay(double val, DateCalendar? calendar = null)
            : this()
        {
            this.Calendar = calendar ?? GetCalendar(val);
            this.Value = val;
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Implicit cast between Julian Day and double
        /// </summary>
        public static implicit operator Double(JulianDay jd)
        {
            return jd.Value;
        }

        /// <summary>
        /// Implicit cast between double and Julian Day
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator JulianDay(Double val)
        {
            return new JulianDay(val);
        }

        /// <summary>
        /// Calendar
        /// </summary>
        public DateCalendar Calendar { get; private set; }

        /// <summary>
        /// The absolute Julian Day value
        /// </summary>
        public double Value { get; private set; }

    }

}
