using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{
    /// <summary>
    /// Valeurs de calcul d'une position de planète
    /// </summary>
    public class PlanetValues
    {
        /// <summary>
        /// Planète
        /// </summary>
        public Planet Planet { get; set; }
        /// <summary>
        /// Nom
        /// </summary>
        public String PlanetName { get; set; }
        /// <summary>
        /// Position dans les maisons
        /// </summary>
        public Double HousePosition { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public Double Longitude { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public Double Latitude { get; set; }
        /// <summary>
        /// Distance
        /// </summary>
        public Double Distance { get; set; }
        /// <summary>
        /// Vitesse de déplacement en longitude
        /// </summary>
        public Double LongitudeSpeed { get; set; }
        /// <summary>
        /// Vitesse de déplacement en latitude
        /// </summary>
        public Double LatitudeSpeed { get; set; }
        /// <summary>
        /// Vitesse de déplacement en distance
        /// </summary>
        public Double DistanceSpeed { get; set; }
        /// <summary>
        /// Error in calculation
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Warning in calculation
        /// </summary>
        public string WarnMessage { get; set; }
    }
}
