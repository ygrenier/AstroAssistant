using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{
    /// <summary>
    /// Astrology engine
    /// </summary>
    public class AstroEngine : IDisposable
    {
        IEphemerisProvider _EphemerisProvider;

        #region Ctors & Dest

        /// <summary>
        /// Nouveau moteur
        /// </summary>
        public AstroEngine(IEphemerisProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            _EphemerisProvider = provider;
        }

        /// <summary>
        /// Libération interne des ressources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _EphemerisProvider.Dispose();
        }

        /// <summary>
        /// Libération des ressources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region Utilitaires

        /// <summary>
        /// Format a value to format : D ° MM' SS.0000
        /// </summary>
        public static string FormatToDegrees(Double value)
        {
            bool minus = value < 0;
            value = Math.Abs(value);
            var deg = (int)value;
            var min = (int)((value * 60.0) % 60.0);
            var sec = ((value * 3600.0) % 60.0);
            return String.Format("{0}{1,3:##0}° {2,2:#0}' {3,7:#0.0000}", minus ? '-' : ' ', deg, min, sec);
        }

        /// <summary>
        /// Format a value to format : HH:mm:ss
        /// </summary>
        public static string FormatToTime(Double value)
        {
            var deg = (int)value;
            value = Math.Abs(value);
            var min = (int)((value * 60.0) % 60.0);
            var sec = (int)((value * 3600.0) % 60.0);
            return String.Format("{0,2:00}:{1:00}:{2:00}", deg, min, sec);
        }

        /// <summary>
        /// Format a value to format : 'HH' h 'mm' m 'ss' s
        /// </summary>
        public static string FormatToHour(Double value)
        {
            var deg = (int)value;
            value = Math.Abs(value);
            var min = (int)((value * 60.0) % 60.0);
            var sec = (int)((value * 3600.0) % 60.0);
            return String.Format("{0,2:#0} h {1:00} m {2:00} s", deg, min, sec);
        }

        static string ZodiacSymbols = "♈♉♊♋♌♍♎♏♐♑♒♓";
        static string[] ZodiacShortNames = new String[]{
            "ar", "ta", "ge", "cn", "le", "vi", 
            "li", "sc", "sa", "cp", "aq", "pi"
        };
        static string[] ZodiacNames = new String[]{
            "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", 
            "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces"
        };

        /// <summary>
        /// Format value to degrees/minutes/seconds
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>d : Degrees</item>
        /// <item>dd : Degrees leading space</item>
        /// <item>ddd : Degrees leading space</item>
        /// <item>dddd : Degrees leading space</item>
        /// <item>a : Absolute Degrees</item>
        /// <item>aa : Absolute Degrees leading space</item>
        /// <item>aaa : Absolute Degrees leading space</item>
        /// <item>n : Zodiac number</item>
        /// <item>nn : Zodiac number leading space</item>
        /// <item>g : Zodiac degrees (degrees % 30)</item>
        /// <item>gg : Zodiac degrees leading space</item>
        /// <item>m : minutes</item>
        /// <item>mm : minutes leading space</item>
        /// <item>s : seconds</item>
        /// <item>ss : seconds leading space</item>
        /// <item>p : seconds decimal part to 0.0 format</item>
        /// <item>pp : seconds decimal part to 0.00 format</item>
        /// <item>ppp : seconds decimal part to 0.000 format</item>
        /// <item>pppp : seconds decimal part to 0.0000 format</item>
        /// <item>ppppp : seconds decimal part to 0.00000 format</item>
        /// <item>z : Zodiac symbol</item>
        /// <item>zz : Zodiac short name</item>
        /// <item>zzz : Zodiac name</item>
        /// <item>- : Minus sign if value is negative</item>
        /// <item>+ : Minus sign if value is negative or space if positive</item>
        /// </list>
        /// <para>
        /// Standard formats are:
        /// <list type="bullet">
        /// <item>D1 : dddd°mm'ss.pppp</item>
        /// <item>D2 : dddd°mm'ss"</item>
        /// <item>Z1 : gg zz mm'ss.pppp</item>
        /// <item>Z2 : gg zz mm'ss"</item>
        /// </list>
        /// </para>
        /// <para>
        /// For d*, a*, n*, g*, m* and s*, the same uppercase format exists for leading 0 instead of space
        /// </para>
        /// </remarks>
        public static String FormatToDegreeMinuteSecond(double value, String format = null)
        {
            if (double.IsNaN(value)) return "nan";
            if (String.IsNullOrEmpty(format)) format = "dddd°mm'ss.pppp";
            switch (format)
            {
                case "D1": format = "dddd°mm'ss.pppp"; break;
                case "D2": format = "dddd°mm'ss\""; break;
                case "Z1": format = "gg zz mm'ss.pppp"; break;
                case "Z2": format = "gg zz mm'ss\""; break;
            }
            // Elements calculation
            var sgn = Math.Sign(value);
            double avalue = Math.Abs(value);
            int deg = (int)value;
            int adeg = (int)avalue;
            int znum = (int)((avalue % 360.0) / 30);
            int zdeg = (int)((avalue % 360.0) % 30.0);
            avalue -= adeg; avalue *= 60.0;
            int min = (int)avalue;
            avalue -= min;
            double dsec = (avalue * 60.0);
            StringBuilder result = new StringBuilder();
            for (int i = 0, fmtLen = format.Length; i < fmtLen; i++)
            {
                char c = format[i];
                int l = 1;
                // Search length of segment
                char[] cf = null;
                switch (c)
                {
                    case 'd':
                    case 'D': cf = new char[] { 'd', 'D' }; break;
                    case 'a':
                    case 'A': cf = new char[] { 'a', 'A' }; break;
                    case 'n':
                    case 'N': cf = new char[] { 'n', 'N' }; break;
                    case 'g':
                    case 'G': cf = new char[] { 'g', 'G' }; break;
                    case 'm':
                    case 'M': cf = new char[] { 'm', 'M' }; break;
                    case 's':
                    case 'S': cf = new char[] { 's', 'S' }; break;
                    case 'p':
                    case 'P': cf = new char[] { 'p', 'P' }; break;
                    case 'z':
                    case 'Z': cf = new char[] { 'z', 'Z' }; break;
                }
                if (cf != null)
                {
                    while (i + 1 < fmtLen && (format[i + 1] == cf[0] || format[i + 1] == cf[1])) { i++; l++; }
                }
                // Format
                switch (c)
                {
                    case 'd': result.AppendFormat(String.Format("{{0,{0}}}", l), deg); break;
                    case 'D': result.AppendFormat(String.Format("{{0:D{0}}}", sgn < 0 ? l - 1 : l), deg); break;
                    case 'a': result.AppendFormat(String.Format("{{0,{0}}}", l), adeg); break;
                    case 'A': result.AppendFormat(String.Format("{{0:D{0}}}", l), adeg); break;
                    case 'n': result.AppendFormat(String.Format("{{0,{0}}}", l), znum); break;
                    case 'N': result.AppendFormat(String.Format("{{0:D{0}}}", l), znum); break;
                    case 'g': result.AppendFormat(String.Format("{{0,{0}}}", l), zdeg); break;
                    case 'G': result.AppendFormat(String.Format("{{0:D{0}}}", l), zdeg); break;
                    case 'm': result.AppendFormat(String.Format("{{0,{0}}}", l), min); break;
                    case 'M': result.AppendFormat(String.Format("{{0:D{0}}}", l), min); break;
                    case 's': result.AppendFormat(String.Format("{{0,{0}}}", l), (int)Math.Round(dsec, l)); break;
                    case 'S': result.AppendFormat(String.Format("{{0:D{0}}}", l), (int)Math.Round(dsec, l)); break;
                    case 'p':
                    case 'P':
                        var t = Math.Round(dsec, l);
                        var prec = t - (int)t;
                        prec = Math.Round(prec * Math.Pow(10, l));
                        result.AppendFormat(String.Format("{{0:D{0}}}", l), (int)prec);
                        break;
                    case 'z':
                    case 'Z':
                        switch (l)
                        {
                            case 1:
                                result.Append(ZodiacSymbols[znum % 12]);
                                break;
                            case 2:
                                result.Append(ZodiacShortNames[znum % 12]);
                                break;
                            default:
                                result.Append(ZodiacNames[znum % 12]);
                                break;
                        }
                        break;
                    case '-':
                        if (sgn < 0) result.Append('-');
                        break;
                    case '+':
                        result.Append((sgn < 0) ? '-' : ' ');
                        break;
                    default:
                        result.Append(c);
                        break;
                }
            }
            return result.ToString();
        }

        #endregion

        #region Calcul de thème

        /// <summary>
        /// Calcul un thème natal
        /// </summary>
        public NatalChart CalculateNatalChart(NatalChartDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException();

            // Préparation du résultat
            NatalChart result = new NatalChart {
                Definition = definition
            };

            // Initialisation du provider pour le calcul
            EphemerisProvider.SetTopographic(definition.PositionCenter, definition.BirthPlacePosition);
            EphemerisProvider.HouseSystem = definition.HouseSystem;

            // Calcul des dates et des temps
            result.JulianDay = EphemerisProvider.ToJulianDay(definition.BirthDate);
            result.UniversalTime = EphemerisProvider.ToUniversalTime(result.JulianDay);
            result.EphemerisTime = EphemerisProvider.ToEphemerisTime(result.JulianDay);
            result.SideralTime = EphemerisProvider.ToSideralTime(result.JulianDay, definition.BirthPlacePosition.Longitude);

            // Calculs
            var enValues = EphemerisProvider.CalcEclipticNutation(result.EphemerisTime);
            result.TrueEclipticObliquity = enValues.TrueEclipticObliquity;
            result.MeanEclipticObliquity = enValues.MeanEclipticObliquity;
            result.NutationLongitude = enValues.NutationLongitude;
            result.NutationObliquity = enValues.NutationObliquity;

            // Planets
            foreach (var planet in definition.Planets)
            {
                if (planet == Planet.EclipticNutation) continue;
                if (planet == Planet.Earth && (definition.PositionCenter == PositionCenter.Geocentric || definition.PositionCenter == PositionCenter.Topocentric))
                    continue;   // Exclude Earth if geo or topo
                var pi = EphemerisProvider.CalcPlanet(planet, result.EphemerisTime, result.ARMC, definition.BirthPlacePosition.Longitude, result.TrueEclipticObliquity);
                result.Planets.Add(pi);
            }
            ///*
            //    //* equator position * /
            //    if (fmt.IndexOfAny("aADdQ".ToCharArray()) >= 0) {
            //        iflag2 = iflag | SwissEph.SEFLG_EQUATORIAL;
            //        if (ipl == SwissEph.SE_FIXSTAR)
            //            iflgret = sweph.swe_fixstar(star, tjd_et, iflag2, xequ, ref serr);
            //        else
            //            iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xequ, ref serr);
            //    }
            //    //* ecliptic cartesian position * /
            //    if (fmt.IndexOfAny("XU".ToCharArray()) >= 0) {
            //        iflag2 = iflag | SwissEph.SEFLG_XYZ;
            //        if (ipl == SwissEph.SE_FIXSTAR)
            //            iflgret = sweph.swe_fixstar(star, tjd_et, iflag2, xcart, ref serr);
            //        else
            //            iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xcart, ref serr);
            //    }
            //    //* equator cartesian position * /
            //    if (fmt.IndexOfAny("xu".ToCharArray()) >= 0) {
            //        iflag2 = iflag | SwissEph.SEFLG_XYZ | SwissEph.SEFLG_EQUATORIAL;
            //        if (ipl == SwissEph.SE_FIXSTAR)
            //            iflgret = sweph.swe_fixstar(star, tjd_et, iflag2, xcartq, ref serr);
            //        else
            //            iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xcartq, ref serr);
            //    }
            //    spnam = se_pname;
            // */

            // Houses
            var houses = EphemerisProvider.CalcHouses(result.JulianDay, definition.BirthPlacePosition.Latitude, definition.BirthPlacePosition.Longitude, result.AscMcs);
            result.Houses.AddRange(houses);

            return result;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Fournisseur d'éphémérides
        /// </summary>
        public IEphemerisProvider EphemerisProvider { get { return _EphemerisProvider; } }

        #endregion

    }

}
