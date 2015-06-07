using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{

    /// <summary>
    /// Extensions des fournisseurs d'éphémérides
    /// </summary>
    public static class EphemerisProviderExtensions
    {

        /// <summary>
        /// Conversion d'une DateDefinition en date Julien
        /// </summary>
        public static JulianDay ToJulianDay(this IEphemerisProvider provider, DateDefinition date, DateCalendar calendar = DateCalendar.Gregorian)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            if (date == null) throw new ArgumentNullException("date");
            return provider.ToJulianDay(date.ToDateTimeOffset(), calendar);
        }

        /// <summary>
        /// Temps sidéral corrigé avec la longitude
        /// </summary>
        public static SideralTime ToSideralTime(this IEphemerisProvider provider, JulianDay jDay, Longitude longitude)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            SideralTime sid = provider.ToSideralTime(jDay) + (longitude / 15.0);
            if (sid >= 24.0) sid -= 24.0;
            if (sid < 0.0) sid += 24.0;
            return sid;
        }
    }

}
