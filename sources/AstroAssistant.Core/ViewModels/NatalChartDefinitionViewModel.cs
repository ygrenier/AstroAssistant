using Astro;
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
        public NatalChartDefinitionViewModel()
        {
            Definition = new NatalChartDefinition();
            Definition.BirthPlacePosition = new GeoPosition();
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public void Initialize(NatalChartDefinition definition)
        {
            Definition = definition ?? new NatalChartDefinition();
            RaisePropertyChanged(() => Definition);
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
                }
            }
        }

        /// <summary>
        /// Décalage horaire
        /// </summary>
        public Double BirthDateUtcOffset
        {
            get { return Definition.BirthDate.UtcOffset.TotalHours; }
            set
            {
                TimeSpan ts = TimeSpan.FromHours(value);
                if (Definition.BirthDate.UtcOffset != ts)
                {
                    Definition.BirthDate.UtcOffset = ts;
                    RaisePropertyChanged(() => BirthDateUtcOffset);
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

        ///// <summary>
        ///// Liste des planètes
        ///// </summary>
        //public List<Planet> Planets { get; private set; }

    }

}
