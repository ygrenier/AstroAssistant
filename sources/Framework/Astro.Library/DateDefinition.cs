using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{

    /// <summary>
    /// Définition d'une date
    /// </summary>
    public class DateDefinition : IEquatable<DateDefinition>
    {
        /// <summary>
        /// Création d'une nouvelle définition de date
        /// </summary>
        public DateDefinition()
        {
        }

        /// <summary>
        /// Création d'une nouvelle définition de date basée sur un DateTimeOffset
        /// </summary>
        public DateDefinition(DateTimeOffset date, DayLightDefinition? dayLight = null)
        {
            SetDate(date, dayLight);
        }

        /// <summary>
        /// Création d'une nouvelle définition de date basée sur un DateTime
        /// </summary>
        public DateDefinition(DateTime date, DayLightDefinition? dayLight = null)
        {
            SetDate(date, dayLight);
        }

        /// <summary>
        /// Test l'égalité
        /// </summary>
        public bool Equals(DateDefinition other)
        {
            if (other == null) return false;
            if (other == this) return true;
            if (this.TimeZone == null && other.TimeZone == null)
            {
                if (this.UtcOffset != other.UtcOffset) return false;
            }
            else if (this.TimeZone != null && other.TimeZone != null)
            {
                if (!String.Equals(this.TimeZone.StandardName, other.TimeZone.StandardName, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else
                return false;
            return this.Year == other.Year
                && this.Month == other.Month
                && this.Day == other.Day
                && this.Hour == other.Hour
                && this.Minute == other.Minute
                && this.Second == other.Second
                && this.MilliSecond == other.MilliSecond
                && this.DayLight == other.DayLight
                ;
        }

        /// <summary>
        /// Défini la date
        /// </summary>
        public void SetDate(DateTimeOffset date, DayLightDefinition? dayLight = null)
        {
            this.Year = date.Year;
            this.Month = date.Month;
            this.Day = date.Day;
            this.Hour = date.Hour;
            this.Minute = date.Minute;
            this.Second = date.Second;
            this.MilliSecond = date.Millisecond;
            this.UtcOffset = date.Offset;
            this.TimeZone = null;
            this.DayLight = dayLight ?? this.DayLight;
        }

        /// <summary>
        /// Défini la date
        /// </summary>
        public void SetDate(DateTime date, DayLightDefinition? dayLight = null)
        {
            this.Year = date.Year;
            this.Month = date.Month;
            this.Day = date.Day;
            this.Hour = date.Hour;
            this.Minute = date.Minute;
            this.Second = date.Second;
            this.MilliSecond = date.Millisecond;
            this.UtcOffset = TimeSpan.Zero;
            this.TimeZone = date.Kind == DateTimeKind.Utc ? TimeZoneInfo.Utc : TimeZoneInfo.Local;
            this.DayLight = dayLight ?? this.DayLight;
        }

        /// <summary>
        /// Calcul l'offset de l'heure d'été
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetDayLightOffset()
        {
            TimeSpan daylightOffset = TimeSpan.Zero;
            switch (DayLight)
            {
                case DayLightDefinition.FromTimeZone:
                    try
                    {
                        if (Year > 0 && TimeZone != null && TimeZone.SupportsDaylightSavingTime && TimeZone.IsDaylightSavingTime(new DateTime(Year, Month, Day, Hour, Minute, Second, MilliSecond)))
                            daylightOffset = TimeSpan.FromHours(1);
                    }
                    catch { }
                    break;
                case DayLightDefinition.On:
                    daylightOffset = TimeSpan.FromHours(1);
                    break;
                case DayLightDefinition.Off:
                default:
                    break;
            }
            return daylightOffset;
        }

        /// <summary>
        /// Calcul l'offset complet
        /// </summary>
        public TimeSpan GetDateOffset()
        {
            var baseOffset = TimeZone != null ? TimeZone.BaseUtcOffset : UtcOffset;
            return baseOffset + GetDayLightOffset();
        }

        /// <summary>
        /// En DateTimeOffset
        /// </summary>
        public DateTimeOffset ToDateTimeOffset()
        {
            return new DateTimeOffset(Year, Month, Day, Hour, Minute, Second, MilliSecond, GetDateOffset());
        }

        /// <summary>
        /// En DateTime
        /// </summary>
        public DateTime ToDateTime()
        {
            return ToDateTimeOffset().DateTime;
        }

        /// <summary>
        /// Année
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Mois
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// Jour
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// Heure
        /// </summary>
        public int Hour { get; set; }
        /// <summary>
        /// Minutes
        /// </summary>
        public int Minute { get; set; }
        /// <summary>
        /// Secondes
        /// </summary>
        public int Second { get; set; }
        /// <summary>
        /// Millisecondes
        /// </summary>
        public int MilliSecond { get; set; }
        /// <summary>
        /// Fuseau horaire
        /// </summary>
        public TimeZoneInfo TimeZone { get; set; }
        /// <summary>
        /// Décalage horaire
        /// </summary>
        public TimeSpan UtcOffset { get; set; }
        /// <summary>
        /// Heure d'été
        /// </summary>
        public DayLightDefinition DayLight { get; set; }

    }

    /// <summary>
    /// Définition de l'heure d'été
    /// </summary>
    public enum DayLightDefinition
    {
        /// <summary>
        /// Calculé depuis les informations du Framework .Net
        /// </summary>
        FromTimeZone,
        /// <summary>
        /// Heure d'hiver
        /// </summary>
        Off,
        /// <summary>
        /// Heure d'été
        /// </summary>
        On
    }

}
