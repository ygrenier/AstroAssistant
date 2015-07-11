using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{

    /// <summary>
    /// Ephemeris provider
    /// </summary>
    public interface IEphemerisProvider : IDisposable
    {

        /// <summary>
        /// Conversion d'un datetimeoffset en date julien
        /// </summary>
        JulianDay ToJulianDay(DateTimeOffset date, DateCalendar calendar = DateCalendar.Gregorian);

        /// <summary>
        /// Conversion d'un jour Julien en temps universel
        /// </summary>
        DateTime ToUniversalTime(JulianDay jDay);

        /// <summary>
        /// Temps éphéméride
        /// </summary>
        EphemerisTime ToEphemerisTime(JulianDay jDay);

        /// <summary>
        /// Temps sidéral
        /// </summary>
        SideralTime ToSideralTime(JulianDay jDay);

        /// <summary>
        /// Définition de la position topographique pour les calculs
        /// </summary>
        void SetTopographic(PositionCenter positionCenter, GeoPosition geoPosition = null);

        /// <summary>
        /// Calcul des nutations écliptiques
        /// </summary>
        EclipticNutationValues CalcEclipticNutation(EphemerisTime time);

        /// <summary>
        /// Calcul les informations d'une planète sans sa position dans une maison
        /// </summary>
        PlanetValues CalcPlanet(Planet planet, EphemerisTime time);

        /// <summary>
        /// Calcul les informations d'une planète avec sa position dans une maison
        /// </summary>
        PlanetValues CalcPlanet(Planet planet, EphemerisTime time, double armc, Longitude longitude, double trueEclipticObliquity);

        /// <summary>
        /// Calcul les positions des maisons
        /// </summary>
        IEnumerable<HouseValues> CalcHouses(JulianDay jDay, Latitude latitude, Longitude longitude, IList<HouseValues> ascmc = null);

        /// <summary>
        /// Système de calcul des maisons
        /// </summary>
        HouseSystem HouseSystem { get; set; }
    }

}
