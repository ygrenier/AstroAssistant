using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Astro
{
    /// <summary>
    /// Longitude
    /// </summary>
    public struct Longitude
    {

        /// <summary>
        /// Create a longitude from a value
        /// </summary>
        /// <param name="value"></param>
        public Longitude(Double value)
            : this()
        {
            var sig = Math.Sign(value);
            value = Math.Abs(value);
            Degrees = (int)value;
            Minutes = ((int)(value * 60.0)) % 60;
            Seconds = ((int)(value * 3600.0)) % 60;
            while (Degrees >= 180) Degrees -= 180;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            if (sig < 0) Value = -Value;
            Polarity = sig < 0 ? LongitudePolarity.West : LongitudePolarity.East;
        }

        /// <summary>
        /// Create a longitude from his components
        /// </summary>
        /// <param name="degrees"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public Longitude(int degrees, int minutes, int seconds)
            : this()
        {
            if (degrees <= -180 || degrees >= 180) throw new ArgumentOutOfRangeException("degrees");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("minutes");
            if (seconds < 0.0 || seconds >= 60.0) throw new ArgumentOutOfRangeException("seconds");
            Degrees = Math.Abs(degrees);
            Minutes = minutes;
            Seconds = seconds;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            if (degrees < 0) Value = -Value;
            Polarity = degrees < 0 ? LongitudePolarity.West : LongitudePolarity.East;
        }

        /// <summary>
        /// Create a longitude from his components
        /// </summary>
        /// <param name="degrees"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="polarity"></param>
        public Longitude(int degrees, int minutes, int seconds, LongitudePolarity polarity)
            : this()
        {
            if (degrees < 0 || degrees >= 180) throw new ArgumentOutOfRangeException("degrees");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("minutes");
            if (seconds < 0.0 || seconds >= 60.0) throw new ArgumentOutOfRangeException("seconds");
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Value = Degrees + (Minutes / 60.0) + (Seconds / 3600.0);
            if (polarity == LongitudePolarity.West) Value = -Value;
            Polarity = polarity;
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override string ToString()
        {
            return String.Format("{0}° {3} {1:D2}' {2:D2}\"", Degrees, Minutes, Seconds, Polarity.ToString()[0]);
        }

        /// <summary>
        /// Converti une représentation chaîne en longitude
        /// </summary>
        public static Longitude Parse(String s)
        {
            Longitude result;
            if (TryParse(s, out result))
                return result;
            throw new FormatException();
        }

        /// <summary>
        /// Converti une représentation chaîne en longitude
        /// </summary>
        public static bool TryParse(String s, out Longitude result)
        {
            String[] patterns = new String[]{
                // XX° E/W XX' XX"
                @"^(?<deg>\d+)°?\s*(?<pol>E|W)\s*(?<min>\d+')?\s*(?<sec>\d+"")?$",
                // XX° XX' XX E/W"
                @"^(?<deg>\d+)°?\s*(?<min>\d+')?\s*(?<sec>\d+"")?\s*(?<pol>E|W)$",
                // +/-XX° XX' XX"
                @"^(?<deg>(?:\+|\-)?\d+)°?\s*(?<min>\d+')?\s*(?<sec>\d+"")?$",
            };

            result = new Longitude();
            if (String.IsNullOrWhiteSpace(s))
                return false;
            foreach (var pattern in patterns)
            {
                Regex re = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                var match = re.Match(s);
                if (match.Success)
                {
                    int deg = int.Parse(match.Groups["deg"].Value);
                    String pol = match.Groups["pol"].Value;
                    String t = match.Groups["min"].Value;
                    int min = String.IsNullOrWhiteSpace(t) ? 0 : int.Parse(t.Substring(0, t.Length - 1));
                    t = match.Groups["sec"].Value;
                    int sec = String.IsNullOrWhiteSpace(t) ? 0 : int.Parse(t.Substring(0, t.Length - 1));
                    try
                    {
                        if (String.IsNullOrWhiteSpace(pol))
                            result = new Longitude(deg, min, sec);
                        else
                            result = new Longitude(deg, min, sec, String.Equals(pol, "W", StringComparison.OrdinalIgnoreCase) ? LongitudePolarity.West : LongitudePolarity.East);
                        return true;
                    }
                    catch { }
                }
            }
            return false;
        }

        /// <summary>
        /// Implicit conversion of Longitude to Double
        /// </summary>
        public static implicit operator Double(Longitude lon)
        {
            return lon.Value;
        }

        /// <summary>
        /// Implicit conversion of Double to Longitude
        /// </summary>
        public static implicit operator Longitude(Double val)
        {
            return new Longitude(val);
        }

        /// <summary>
        /// Degrees
        /// </summary>
        public int Degrees { get; private set; }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get; private set; }

        /// <summary>
        /// Seconds
        /// </summary>
        public int Seconds { get; private set; }

        /// <summary>
        /// Polarity
        /// </summary>
        public LongitudePolarity Polarity { get; private set; }

        /// <summary>
        /// Numeric value
        /// </summary>
        public Double Value { get; private set; }

    }

    /// <summary>
    /// Longitude polarity
    /// </summary>
    public enum LongitudePolarity
    {
        /// <summary>
        /// East (Positive)
        /// </summary>
        East,
        /// <summary>
        /// West (Negative)
        /// </summary>
        West
    }
}
