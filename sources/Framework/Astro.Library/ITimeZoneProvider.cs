using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{
    /// <summary>
    /// Fournisseur de fuseau horaire
    /// </summary>
    public interface ITimeZoneProvider
    {
        /// <summary>
        /// Recherche un fuseau horaire
        /// </summary>
        TimeZoneInfo FindTimeZone(string name);
    }
}
