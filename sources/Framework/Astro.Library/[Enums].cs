using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{

    /// <summary>
    /// Position center
    /// </summary>
    public enum PositionCenter
    {
        Geocentric,
        Topocentric,
        Heliocentric,
        Barycentric,
        SiderealFagan,
        SiderealLahiri
    }

    /// <summary>
    /// House system calculation
    /// </summary>
    public enum HouseSystem : sbyte
    {
        Placidus,
        Koch,
        Porphyrius,
        Regiomontanus,
        Campanus,
        Equal,
        VehlowEqual,
        WholeSign,
        MeridianSystem,
        Horizon,
        PolichPage,
        Alcabitus,
        Morinus,
        KrusinskiPisa,
        GauquelinSector,
        APC
    }

}
