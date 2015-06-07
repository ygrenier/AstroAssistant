using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{
    /// <summary>
    /// Valeurs
    /// </summary>
    public class EclipticNutationValues
    {
        /// <summary>
        /// Mean ecliptic obliquity
        /// </summary>
        public Double MeanEclipticObliquity { get; set; }

        /// <summary>
        /// True ecliptic obliquity
        /// </summary>
        public Double TrueEclipticObliquity { get; set; }

        /// <summary>
        /// Nutation in longitude
        /// </summary>
        public Double NutationLongitude { get; set; }

        /// <summary>
        /// Nutation in obliquity
        /// </summary>
        public Double NutationObliquity { get; set; }

    }
}
