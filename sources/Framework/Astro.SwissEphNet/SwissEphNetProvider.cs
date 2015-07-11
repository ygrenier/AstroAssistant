using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro.SwissEphNet
{

    /// <summary>
    /// SwissEphNet Ephemeris Provider
    /// </summary>
    public class SwissEphNetProvider : IEphemerisProvider
    {
        int _SwephFlag = -1;

        #region Ctors & Dest

        /// <summary>
        /// Create SwissEphNet provider
        /// </summary>
        public SwissEphNetProvider()
        {
            _SwephFlag = -1;
            HouseSystem = Astro.HouseSystem.Placidus;
            Sweph = new SwissEph();
            RecalcSwephState();
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Sweph.Dispose();
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region Initialize & Config

        /// <summary>
        /// Check if provider initialized
        /// </summary>
        protected void CheckInitialized()
        {
            if (_SwephFlag < 0)
            {
                _SwephFlag = 0;
                Initialize();
            }
        }

        /// <summary>
        /// Initialize provider
        /// </summary>
        protected virtual void Initialize()
        {
            RecalcSwephState();
        }

        /// <summary>
        /// Recalculate the swisseph flag and parameters
        /// </summary>
        protected void RecalcSwephState()
        {
            _SwephFlag = SwissEph.SEFLG_SPEED;
            // Ephemeris type
            switch (Ephemeris)
            {
                case EphemerisMode.Moshier:
                    _SwephFlag |= SwissEph.SEFLG_MOSEPH;
                    break;
                case EphemerisMode.JPL:
                    _SwephFlag |= SwissEph.SEFLG_JPLEPH;
                    break;
                case EphemerisMode.SwissEphemeris:
                default:
                    _SwephFlag |= SwissEph.SEFLG_SWIEPH;
                    break;
            }
            // Position center
            var sidmode = SwissEph.SE_SIDM_FAGAN_BRADLEY;
            switch (PositionCenter)
            {
                case PositionCenter.Topocentric:
                    _SwephFlag |= SwissEph.SEFLG_TOPOCTR;
                    break;
                case PositionCenter.Heliocentric:
                    _SwephFlag |= SwissEph.SEFLG_HELCTR;
                    break;
                case PositionCenter.Barycentric:
                    _SwephFlag |= SwissEph.SEFLG_BARYCTR;
                    break;
                case PositionCenter.SiderealFagan:
                    _SwephFlag |= SwissEph.SEFLG_SIDEREAL;
                    sidmode = SwissEph.SE_SIDM_FAGAN_BRADLEY;
                    break;
                case PositionCenter.SiderealLahiri:
                    _SwephFlag |= SwissEph.SEFLG_SIDEREAL;
                    sidmode = SwissEph.SE_SIDM_LAHIRI;
                    break;
                case PositionCenter.Geocentric:
                default:
                    break;
            }
            Sweph.swe_set_sid_mode(sidmode, 0, 0);
        }

        /// <summary>
        /// Définition de la position topographique pour les calculs
        /// </summary>
        public void SetTopographic(PositionCenter positionCenter, GeoPosition geoPosition = null)
        {
            if (positionCenter == PositionCenter.Topocentric)
            {
                if (geoPosition == null)
                    throw new ArgumentException("Topographic center require a geographic position", "geoPosition");
                TopographicPositionCenter = geoPosition;
                RecalcSwephState();
                Sweph.swe_set_topo(TopographicPositionCenter.Longitude, TopographicPositionCenter.Latitude, TopographicPositionCenter.Altitude);
            }
            else
            {
                PositionCenter = positionCenter;
                TopographicPositionCenter = geoPosition;
                RecalcSwephState();
            }
        }

        #endregion

        #region Calcul de dates et de temps

        static int ToCalendar(DateCalendar calendar)
        {
            return calendar == DateCalendar.Gregorian ? SwissEph.SE_GREG_CAL : SwissEph.SE_JUL_CAL;
        }

        /// <summary>
        /// Conversion d'un datetimeoffset en date julien
        /// </summary>
        public JulianDay ToJulianDay(DateTimeOffset date, DateCalendar calendar = DateCalendar.Gregorian)
        {
            var utc = date.ToUniversalTime();
            return new JulianDay(
                Sweph.swe_julday(
                    utc.Year,
                    utc.Month,
                    utc.Day,
                    utc.TimeOfDay.TotalHours,
                    ToCalendar(calendar)
                    ),
                calendar);
        }

        /// <summary>
        /// Conversion d'une date Julien en temps universel
        /// </summary>
        public DateTime ToUniversalTime(JulianDay jDay)
        {
            int year = 0, month = 0, day = 0, hour = 0, minute = 0; double second = 0;
            Sweph.swe_jdet_to_utc(jDay.Value, ToCalendar(jDay.Calendar), ref year, ref month, ref day, ref hour, ref minute, ref second);
            return new DateTime(year, month, day, hour, minute, (int)second, DateTimeKind.Utc);
        }

        /// <summary>
        /// Temps éphéméride
        /// </summary>
        public EphemerisTime ToEphemerisTime(JulianDay jDay)
        {
            return new EphemerisTime(jDay, Sweph.swe_deltat(jDay));
        }

        /// <summary>
        /// Temps sidéral
        /// </summary>
        public SideralTime ToSideralTime(JulianDay jDay)
        {
            return Sweph.swe_sidtime(jDay);
        }

        #endregion

        #region Calcul planétaires

        /// <summary>
        /// Calcul des nutations écliptiques
        /// </summary>
        public EclipticNutationValues CalcEclipticNutation(EphemerisTime time)
        {
            CheckInitialized();
            String serr = null;
            Double[] x = new double[24];
            var iflgret = Sweph.swe_calc(time, SwissEph.SE_ECL_NUT, _SwephFlag, x, ref serr);
            return new EclipticNutationValues {
                TrueEclipticObliquity = x[0],
                MeanEclipticObliquity = x[1],
                NutationLongitude = x[2],
                NutationObliquity = x[3]
            };
        }

        PlanetValues InternalCalcPlanet(Planet planet, EphemerisTime time, double? armc = null, Longitude? longitude = null, double? trueEclipticObliquity = null)
        {
            CheckInitialized();
            String serr = null;
            Double[] x = new double[24];
            String star = String.Empty;
            int iflgret;
            var result = new PlanetValues() {
                Planet = planet
            };
            if (planet == Planet.FixedStar)
            {
                iflgret = Sweph.swe_fixstar(star, time, _SwephFlag, x, ref serr);
                result.PlanetName = star;
            }
            else
            {
                iflgret = Sweph.swe_calc(time, planet, _SwephFlag, x, ref serr);
                result.PlanetName = Sweph.swe_get_planet_name(planet);
                if (planet.IsAsteroid)
                {
                    result.PlanetName = String.Format("#{0}", planet - Planet.FirstAsteroid);
                }
            }
            if (iflgret >= 0)
            {
                result.Longitude = x[0];
                result.Latitude = x[1];
                result.Distance = x[2];
                result.LongitudeSpeed = x[3];
                result.LatitudeSpeed = x[4];
                result.DistanceSpeed = x[5];
                if (armc.HasValue && longitude.HasValue && trueEclipticObliquity.HasValue)
                {
                    result.HousePosition = Sweph.swe_house_pos(armc.Value, longitude.Value, trueEclipticObliquity.Value, HouseSystemToChar(_HouseSystem), x, ref serr);
                    if (result.HousePosition == 0)
                        iflgret = SwissEph.ERR;
                }
            }
            if (iflgret < 0)
            {
                if (!String.IsNullOrEmpty(serr))
                {
                    result.ErrorMessage = serr;
                }
            }
            else if (!String.IsNullOrEmpty(serr) && String.IsNullOrEmpty(result.WarnMessage))
                result.WarnMessage = serr;
            return result;
        }

        /// <summary>
        /// Calcul les informations d'une planète sans sa position dans une maison
        /// </summary>
        public PlanetValues CalcPlanet(Planet planet, EphemerisTime time)
        {
            return InternalCalcPlanet(planet, time);
        }

        /// <summary>
        /// Calcul les informations d'une planète avec sa position dans une maison
        /// </summary>
        public PlanetValues CalcPlanet(Planet planet, EphemerisTime time, double armc, Longitude longitude, double trueEclipticObliquity)
        {
            return InternalCalcPlanet(planet, time, armc, longitude, trueEclipticObliquity);
        }

        #endregion

        #region Calcul des maisons

        /// <summary>
        /// Convert a char to an house system
        /// </summary>
        public static Char HouseSystemToChar(HouseSystem hs)
        {
            switch (hs)
            {
                case HouseSystem.Koch: return 'K';
                case HouseSystem.Porphyrius: return 'O';
                case HouseSystem.Regiomontanus: return 'R';
                case HouseSystem.Campanus: return 'C';
                case HouseSystem.Equal: return 'E';
                case HouseSystem.VehlowEqual: return 'V';
                case HouseSystem.WholeSign: return 'W';
                case HouseSystem.MeridianSystem: return 'X';
                case HouseSystem.Horizon: return 'H';
                case HouseSystem.PolichPage: return 'T';
                case HouseSystem.Alcabitus: return 'B';
                case HouseSystem.Morinus: return 'M';
                case HouseSystem.KrusinskiPisa: return 'U';
                case HouseSystem.GauquelinSector: return 'G';
                case HouseSystem.APC: return 'Y';
                case HouseSystem.Placidus:
                default: return 'P';
            }
        }

        /// <summary>
        /// Calcul des maisons et des ascdendants
        /// </summary>
        public IEnumerable<HouseValues> CalcHouses(JulianDay jDay, Latitude latitude, Longitude longitude, IList<HouseValues> ascmc = null)
        {
            // Houses
            double[] cusps = new double[HouseSystem == HouseSystem.GauquelinSector ? 37 : 13];
            double[] ascmcCusps = new double[10];
            String serr = null;
            var hNames = new String[] { 
                "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", 
                "IX", "X", "XI", "XII", "XII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", 
                "XX", "XXI", "XXII", "XXII", "XXIV", "XXV", "XXVI", "XXVII", "XXVIII", "XXIX", 
                "XXX", "XXXI", "XXXII", "XXXII", "XXXIV", "XXXV", "XXXVI", "XXXVII", "XXXVIII", "XXXIX"
            };
            var amNames = new String[] { "Ascendant", "MC", "ARMC", "Vertex", "Equatorial ascendant", 
                    "Co-ascendant (Walter Koch)", "Co-ascendant (Michael Munkasey)", "Polar ascendant (M. Munkasey)" };
            var iflgret = Sweph.swe_houses_ex(jDay, _SwephFlag, latitude, longitude, HouseSystemToChar(HouseSystem), cusps, ascmcCusps);
            if (iflgret < 0)
                throw new SwissEphNetError(serr);
            if (ascmc != null)
            {
                for (int i = 0; i < 7; i++)
                {
                    ascmc.Add(new HouseValues {
                        House = i,
                        HouseName = amNames[i],
                        Cusp = ascmcCusps[i]
                    });
                }
            }
            for (int i = 1; i < cusps.Length; i++)
            {
                yield return new HouseValues() {
                    House = i,
                    HouseName = hNames[i],
                    Cusp = cusps[i]
                };
            }
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Objet SwissEphemeris
        /// </summary>
        public SwissEph Sweph { get; private set; }

        /// <summary>
        /// Current position center
        /// </summary>
        public PositionCenter PositionCenter { get; private set; }

        /// <summary>
        /// Topographic
        /// </summary>
        public GeoPosition TopographicPositionCenter { get; private set; }

        /// <summary>
        /// Ephemeris mode used
        /// </summary>
        public EphemerisMode Ephemeris
        {
            get { return _Ephemeris; }
            set
            {
                if (_Ephemeris != value)
                {
                    _Ephemeris = value;
                    RecalcSwephState();
                }
            }
        }
        private EphemerisMode _Ephemeris;

        /// <summary>
        /// Current house system
        /// </summary>
        public HouseSystem HouseSystem
        {
            get { return _HouseSystem; }
            set
            {
                if (_HouseSystem != value)
                {
                    _HouseSystem = value;
                    RecalcSwephState();
                }
            }
        }
        private HouseSystem _HouseSystem;

        #endregion

    }

}
