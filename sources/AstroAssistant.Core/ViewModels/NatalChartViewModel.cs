using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroAssistant.ViewModels
{

    /// <summary>
    /// ViewModel d'un thème astral
    /// </summary>
    public class NatalChartViewModel : ViewModel
    {
        public NatalChartViewModel()
        {
            //DateUT = new DateUT(DateTime.Now);
            //Longitude = new SweNet.Longitude(5, 20, 0, LongitudePolarity.East);
            //Latitude = new SweNet.Latitude(47, 52, 0, LatitudePolarity.North);
            //HouseSystem = HouseSystem.Placidus;
            //Planets.AddRange(new Planet[] { 
            //    Planet.Sun, Planet.Moon, Planet.Mercury, Planet.Venus, Planet.Mars, Planet.Jupiter, 
            //    Planet.Saturn, Planet.Uranus, Planet.Neptune, Planet.Pluto,
            //    Planet.MeanNode, Planet.TrueNode,
            //    Planet.MeanApog, Planet.OscuApog, Planet.Earth
            //});
            //Planets.AddRange(new Planet[] { Planet.AsAsteroid(433), Planet.AsAsteroid(3045), Planet.AsAsteroid(7066) });
            FileName = null;
            IsDirty = false;
        }

        /// <summary>
        /// Nom du fichier
        /// </summary>
        public String FileName
        {
            get { return _FileName; }
            private set { SetProperty(ref _FileName, value, () => FileName); }
        }
        private String _FileName;

        /// <summary>
        /// Indique si le modèle doit être enregistré
        /// </summary>
        public bool IsDirty
        {
            get { return _IsDirty; }
            private set { SetProperty(ref _IsDirty, value, () => IsDirty); }
        }
        private bool _IsDirty;

    }

}
