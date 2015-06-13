using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.Services
{
    /// <summary>
    /// Fournisseur de TimeZone
    /// </summary>
    public class TimeZoneProvider : ITimeZoneProvider
    {
        /// <summary>
        /// Recherche un fuseau horaire
        /// </summary>
        public TimeZoneInfo FindTimeZone(string name)
        {
            return
                TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => String.Equals(tz.Id, name, StringComparison.OrdinalIgnoreCase))
                ?? TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => String.Equals(tz.StandardName, name, StringComparison.OrdinalIgnoreCase) || String.Equals(tz.DisplayName, name, StringComparison.OrdinalIgnoreCase))
                ?? TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(tz => tz.StandardName.ToLower().Contains(name.ToLower()) || tz.DisplayName.ToLower().Contains(name.ToLower()))
                ;
        }
    }

}
