using Astro;
using AstroAssistant.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.ViewModels
{

    /// <summary>
    /// ViewModel pour la définition d'un thème
    /// </summary>
    public class NatalChartDefinitionViewModel : ViewModel, AstroAssistant.ViewModels.INatalChartDefinitionViewModel
    {
        /// <summary>
        /// Création du viewmodel
        /// </summary>
        public NatalChartDefinitionViewModel(ITimeZoneProvider tzProvider)
        {
            Definition = new NatalChartDefinition();
            Definition.BirthPlacePosition = new GeoPosition();
            Definition.BirthDate.SetDate(DateTimeOffset.Now);
            this.BirthDateTimeZone = TimeZoneInfo.Local;

            // Liste des genres
            ListGenders = new List<KeyValuePair<Gender, string>>();
            ListGenders.Add(new KeyValuePair<Gender, string>(Gender.Female, Locales.Gender_Female_Caption));
            ListGenders.Add(new KeyValuePair<Gender, string>(Gender.Male, Locales.Gender_Male_Caption));

            // Liste des mode d'heures d'été
            ListDayLightDefinitions = new List<KeyValuePair<DayLightDefinition, string>>();
            ListDayLightDefinitions.Add(new KeyValuePair<DayLightDefinition, string>(DayLightDefinition.FromTimeZone, Locales.DayLight_FromTimeZone_Caption));
            ListDayLightDefinitions.Add(new KeyValuePair<DayLightDefinition, string>(DayLightDefinition.On, Locales.DayLight_On_Caption));
            ListDayLightDefinitions.Add(new KeyValuePair<DayLightDefinition, string>(DayLightDefinition.Off, Locales.DayLight_Off_Caption));

            // Liste des fuseaux horaire
            ListTimeZoneInfos = new List<KeyValuePair<TimeZoneInfo, String>>();
            ListTimeZoneInfos.Add(new KeyValuePair<TimeZoneInfo, string>(null, Locales.TimeZone_Custom_Caption));
            if (tzProvider != null)
                ListTimeZoneInfos.AddRange(tzProvider.GetTimeZones().Select(tz => new KeyValuePair<TimeZoneInfo, String>(tz, tz.DisplayName)));

            // Liste des systèmes de maisons
            ListHouseSystems = new List<KeyValuePair<HouseSystem, string>>();
            foreach (HouseSystem hs in Enum.GetValues(typeof(HouseSystem)))
                ListHouseSystems.Add(new KeyValuePair<HouseSystem, string>(hs, hs.GetCaptionString()));

            // Liste des positions
            ListPositionCenters = new List<KeyValuePair<PositionCenter, string>>();
            foreach (PositionCenter pc in Enum.GetValues(typeof(PositionCenter)))
                ListPositionCenters.Add(new KeyValuePair<PositionCenter, string>(pc, pc.GetCaptionString()));

        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public void Initialize(NatalChartDefinition definition)
        {
            Definition = definition ?? new NatalChartDefinition();
            RaisePropertyChanged("");
        }

        /// <summary>
        /// Définition du thème
        /// </summary>
        public NatalChartDefinition Definition { get; private set; }

        /// <summary>
        /// Nom de la personne
        /// </summary>
        public String Name
        {
            get { return Definition.Name; }
            set
            {
                if (Definition.Name != value)
                {
                    Definition.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        /// <summary>
        /// Genre
        /// </summary>
        public Gender Gender
        {
            get { return Definition.Gender; }
            set
            {
                if (Definition.Gender != value)
                {
                    Definition.Gender = value;
                    RaisePropertyChanged(() => Gender);
                }
            }
        }

        /// <summary>
        /// Liste des genres
        /// </summary>
        public List<KeyValuePair<Gender,String>> ListGenders { get; private set; }

        void RaiseDateProperties()
        {
            RaisePropertyChanged(() => BirthDayLightOffset);
            RaisePropertyChanged(() => BirthDateUTC);
        }

        /// <summary>
        /// Année
        /// </summary>
        public int BirthDateYear
        {
            get { return Definition.BirthDate.Year; }
            set
            {
                if (Definition.BirthDate.Year != value)
                {
                    Definition.BirthDate.Year = value;
                    RaisePropertyChanged(() => BirthDateYear);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Mois
        /// </summary>
        public int BirthDateMonth
        {
            get { return Definition.BirthDate.Month; }
            set
            {
                if (Definition.BirthDate.Month != value)
                {
                    Definition.BirthDate.Month = value;
                    RaisePropertyChanged(() => BirthDateMonth);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Jour
        /// </summary>
        public int BirthDateDay
        {
            get { return Definition.BirthDate.Day; }
            set
            {
                if (Definition.BirthDate.Day != value)
                {
                    Definition.BirthDate.Day = value;
                    RaisePropertyChanged(() => BirthDateDay);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Heure
        /// </summary>
        public int BirthDateHour
        {
            get { return Definition.BirthDate.Hour; }
            set
            {
                if (Definition.BirthDate.Hour != value)
                {
                    Definition.BirthDate.Hour = value;
                    RaisePropertyChanged(() => BirthDateHour);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Minutes
        /// </summary>
        public int BirthDateMinute
        {
            get { return Definition.BirthDate.Minute; }
            set
            {
                if (Definition.BirthDate.Minute != value)
                {
                    Definition.BirthDate.Minute = value;
                    RaisePropertyChanged(() => BirthDateMinute);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Secondes
        /// </summary>
        public int BirthDateSecond
        {
            get { return Definition.BirthDate.Second; }
            set
            {
                if (Definition.BirthDate.Second != value)
                {
                    Definition.BirthDate.Second = value;
                    RaisePropertyChanged(() => BirthDateSecond);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Millisecondes
        /// </summary>
        public int BirthDateMilliSecond
        {
            get { return Definition.BirthDate.MilliSecond; }
            set
            {
                if (Definition.BirthDate.MilliSecond != value)
                {
                    Definition.BirthDate.MilliSecond = value;
                    RaisePropertyChanged(() => BirthDateMilliSecond);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Fuseau horaire
        /// </summary>
        public TimeZoneInfo BirthDateTimeZone
        {
            get { return Definition.BirthDate.TimeZone; }
            set
            {
                if (Definition.BirthDate.TimeZone != value)
                {
                    Definition.BirthDate.TimeZone = value;
                    RaisePropertyChanged(() => BirthDateTimeZone);
                    RaisePropertyChanged(() => BirthDateUtcOffset);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Liste des fuseaux horaire
        /// </summary>
        public List<KeyValuePair<TimeZoneInfo, String>> ListTimeZoneInfos { get; private set; }

        /// <summary>
        /// Décalage horaire
        /// </summary>
        public Double BirthDateUtcOffset
        {
            get { return Definition.BirthDate.GetDateOffset().TotalHours; }
            set
            {
                TimeSpan ts = TimeSpan.FromHours(value);
                if (Definition.BirthDate.UtcOffset != ts)
                {
                    Definition.BirthDate.UtcOffset = ts;
                    RaisePropertyChanged(() => BirthDateUtcOffset);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Heure d'été
        /// </summary>
        public DayLightDefinition BirthDateDayLight
        {
            get { return Definition.BirthDate.DayLight; }
            set
            {
                if (Definition.BirthDate.DayLight != value)
                {
                    Definition.BirthDate.DayLight = value;
                    RaisePropertyChanged(() => BirthDateDayLight);
                    RaiseDateProperties();
                }
            }
        }

        /// <summary>
        /// Offset d'heure d'été
        /// </summary>
        public double BirthDayLightOffset { get { return Definition.BirthDate.GetDayLightOffset().TotalHours; } }

        /// <summary>
        /// Liste des mode de gestion des heures d'été
        /// </summary>
        public List<KeyValuePair<Astro.DayLightDefinition, String>> ListDayLightDefinitions { get; private set; }

        /// <summary>
        /// Date UTC
        /// </summary>
        public DateTimeOffset BirthDateUTC
        {
            get
            {
                try
                {
                    return Definition.BirthDate.ToDateTimeOffset().ToUniversalTime();
                }
                catch
                {
                    return DateTimeOffset.MinValue;
                }
            }
        }

        /// <summary>
        /// Nom du lieu de naissance
        /// </summary>
        public String BirthPlaceName
        {
            get { return Definition.BirthPlaceName; }
            set
            {
                if (Definition.BirthPlaceName != value)
                {
                    Definition.BirthPlaceName = value;
                    RaisePropertyChanged(() => BirthPlaceName);
                }
            }
        }

        /// <summary>
        /// Position géographique du lieu de naissance
        /// </summary>
        public GeoPosition BirthPlacePosition
        {
            get { return Definition.BirthPlacePosition; }
            set
            {
                if (Definition.BirthPlacePosition != value)
                {
                    Definition.BirthPlacePosition = value ?? new GeoPosition();
                    RaisePropertyChanged(() => BirthPlacePosition);
                }
            }
        }

        /// <summary>
        /// Longitude
        /// </summary>
        public Longitude BirthPlaceLongitude
        {
            get { return BirthPlacePosition.Longitude; }
            set
            {
                if (BirthPlacePosition.Longitude != value)
                {
                    BirthPlacePosition.Longitude = value;
                    RaisePropertyChanged(() => BirthPlaceLongitude);
                }
            }
        }

        /// <summary>
        /// Latitude
        /// </summary>
        public Latitude BirthPlaceLatitude
        {
            get { return BirthPlacePosition.Latitude; }
            set
            {
                if (BirthPlacePosition.Latitude != value)
                {
                    BirthPlacePosition.Latitude = value;
                    RaisePropertyChanged(() => BirthPlaceLatitude);
                }
            }
        }

        /// <summary>
        /// Altitude
        /// </summary>
        public Double BirthPlaceAltitude
        {
            get { return Definition.BirthPlacePosition.Altitude; }
            set
            {
                if (BirthPlacePosition.Altitude != value)
                {
                    BirthPlacePosition.Altitude = value;
                    RaisePropertyChanged(() => BirthPlaceAltitude);
                }
            }
        }

        /// <summary>
        /// Position Center
        /// </summary>
        public PositionCenter PositionCenter
        {
            get { return Definition.PositionCenter; }
            set
            {
                if (Definition.PositionCenter != value)
                {
                    Definition.PositionCenter = value;
                    RaisePropertyChanged(() => PositionCenter);
                }
            }
        }

        /// <summary>
        /// Liste des positions
        /// </summary>
        public List<KeyValuePair<Astro.PositionCenter, String>> ListPositionCenters { get; private set; }

        /// <summary>
        /// Calcul des maisons
        /// </summary>
        public HouseSystem HouseSystem
        {
            get { return Definition.HouseSystem; }
            set
            {
                if (Definition.HouseSystem != value)
                {
                    Definition.HouseSystem = value;
                    RaisePropertyChanged(() => HouseSystem);
                }
            }
        }

        /// <summary>
        /// Liste des systèmes de calcul des maisons
        /// </summary>
        public List<KeyValuePair<Astro.HouseSystem, String>> ListHouseSystems { get; private set; }

        ///// <summary>
        ///// Liste des planètes
        ///// </summary>
        //public List<Planet> Planets { get; private set; }

    }

}
