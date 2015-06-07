using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{

    /// <summary>
    /// Définition d'un thème natal
    /// </summary>
    public class NatalChartDefinition
    {

        /// <summary>
        /// Création d'une définition d'un thème natal
        /// </summary>
        public NatalChartDefinition()
        {
            BirthDate = new DateDefinition();
        }

        /// <summary>
        /// Nom de la personne
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Date de naissance
        /// </summary>
        public DateDefinition BirthDate { get; private set; }

        /// <summary>
        /// Nom du lieu de naissance
        /// </summary>
        public String BirthPlaceName { get; set; }

        /// <summary>
        /// Position géographique du lieu de naissance
        /// </summary>
        public GeoPosition BirthPlacePosition { get; set; }
    }

}
