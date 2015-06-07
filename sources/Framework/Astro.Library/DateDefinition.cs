using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{

    /// <summary>
    /// Définition d'une date
    /// </summary>
    public class DateDefinition
    {

        /// <summary>
        /// Calcul l'offset complet
        /// </summary>
        public TimeSpan GetDateOffset()
        {
            var baseOffset = TimeZone != null ? TimeZone.BaseUtcOffset : UtcOffset;
            TimeSpan daylightOffset = TimeSpan.Zero;
            switch (DayLight)
            {
                case DayLightDefinition.FromDotNet:
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
            return baseOffset + daylightOffset;
        }

        /// <summary>
        /// En DateTimeOffset
        /// </summary>
        public DateTimeOffset ToDateTimeOffset()
        {
            return new DateTimeOffset(Year, Month, Day, Hour, Minute, Second, MilliSecond, GetDateOffset());
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
        FromDotNet,
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
