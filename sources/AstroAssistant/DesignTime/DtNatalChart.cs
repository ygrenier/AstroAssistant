using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.DesignTime
{
#if DEBUG
    public class DtNatalChart : NatalChart
    {
        public DtNatalChart()
        {
            var dt = DateTime.Now;
            this.Definition = new NatalChartDefinition();
            this.Definition.BirthDate.SetDate(dt);
            this.Definition.BirthDate.TimeZone = TimeZoneInfo.Local;
            this.Definition.BirthPlaceName = "Besançon";
            this.Definition.BirthPlacePosition = new Astro.GeoPosition(new Longitude(6, 1, 19, LongitudePolarity.East), new Latitude(47, 14, 35, LatitudePolarity.North), 400);
            this.Definition.Gender = Gender.Male;
            this.Definition.HouseSystem = HouseSystem.Placidus;
            this.Definition.Name = "Test";
            this.Definition.PositionCenter = PositionCenter.Topocentric;

            this.JulianDay = JulianDay.J2000;
            this.EphemerisTime = new EphemerisTime(this.JulianDay, 12.34);
            this.UniversalTime = this.Definition.BirthDate.ToDateTime().ToUniversalTime();
            this.SideralTime = new SideralTime(this.JulianDay.Value);
            this.MeanEclipticObliquity = 98.54;
            this.NutationLongitude = 66.77;
            this.NutationObliquity = 88.99;
            
            for (int i = 0; i < 10; i++)
            {
                this.AscMcs.Add(new HouseValues {
                    Cusp = i * 1.7,
                    House = i + 1,
                    HouseName = String.Format("Maison {0}", i + 1)
                });
            }

            for (int i = 0; i < 10; i++)
            {
                this.Houses.Add(new HouseValues {
                    Cusp = i * 1.7,
                    House = i + 1,
                    HouseName = String.Format("Maison {0}", i + 1)
                });
            }

            for (int i = 0; i < 10; i++)
            {
                this.Planets.Add(new PlanetValues {
                    Planet=new Planet(i),
                    PlanetName = String.Format("Planète {0}", i + 1),
                    Distance=12.34,
                    DistanceSpeed=0.5,
                    Latitude=12,
                    LatitudeSpeed=6,
                    Longitude=23,
                    LongitudeSpeed=7,
                    HousePosition=23*i,
                    ErrorMessage = (i % 7 == 0) ? "Erreur lors du calcul" : null,
                    WarnMessage = (i % 8 == 0) ? "Alerte lors du calcul" : null
                });
            }

        }
    }
#endif
}
