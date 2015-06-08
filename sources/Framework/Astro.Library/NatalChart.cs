using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{
    /// <summary>
    /// Thème natal
    /// </summary>
    public class NatalChart
    {
        /// <summary>
        /// Création d'un nouveau thème natal
        /// </summary>
        public NatalChart()
        {
            Planets = new List<PlanetValues>();
            Houses = new List<HouseValues>();
            AscMcs = new List<HouseValues>();
        }

        /// <summary>
        /// Définition du thème
        /// </summary>
        public NatalChartDefinition Definition { get; set; }

        /// <summary>
        /// Date Julien
        /// </summary>
        public JulianDay JulianDay { get; set; }

        /// <summary>
        /// Temps universel
        /// </summary>
        public DateTime UniversalTime { get; set; }

        /// <summary>
        /// Temps éphémérides
        /// </summary>
        public EphemerisTime EphemerisTime { get; set; }

        /// <summary>
        /// Delat T in seconds
        /// </summary>
        public Double DeltaTSec { get { return EphemerisTime.DeltaT * 86400.0; } }

        /// <summary>
        /// Sideral time
        /// </summary>
        public double SideralTime { get; set; }

        /// <summary>
        /// Sideral time in degrees
        /// </summary>
        public double SideralTimeInDegrees { get { return SideralTime * 15; } }

        /// <summary>
        /// ARMC : Sideral time in degrees
        /// </summary>
        public double ARMC { get { return SideralTime * 15; } }

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

        /// <summary>
        /// Liste des planètes
        /// </summary>
        public List<PlanetValues> Planets { get; private set; }

        /// <summary>
        /// Maisons
        /// </summary>
        public List<HouseValues> Houses { get; private set; }

        /// <summary>
        /// Ascendants
        /// </summary>
        public List<HouseValues> AscMcs { get; private set; }

    }
}
