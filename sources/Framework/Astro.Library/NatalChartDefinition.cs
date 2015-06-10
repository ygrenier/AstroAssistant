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
            HouseSystem = Astro.HouseSystem.Placidus;
            Planets = new List<Planet>();
            SetDefaultPlanets();
        }

        /// <summary>
        /// Défini la liste des planètes par défaut
        /// </summary>
        public void SetDefaultPlanets()
        {
            Planets.Clear();
            Planets.AddRange(new Planet[] { 
                Planet.Sun, Planet.Moon, Planet.Mercury, Planet.Venus, Planet.Mars, Planet.Jupiter, 
                Planet.Saturn, Planet.Uranus, Planet.Neptune, Planet.Pluto,
                Planet.MeanNode, Planet.TrueNode,
                Planet.MeanApog, Planet.OscuApog, Planet.Earth
            });
        }

        /// <summary>
        /// Nom de la personne
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Genre
        /// </summary>
        public Gender Gender { get; set; }

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

        /// <summary>
        /// Position Center
        /// </summary>
        public PositionCenter PositionCenter { get; set; }

        /// <summary>
        /// Calcul des maisons
        /// </summary>
        public HouseSystem HouseSystem { get; set; }

        /// <summary>
        /// Liste des planètes
        /// </summary>
        public List<Planet> Planets { get; private set; }

    }

}
