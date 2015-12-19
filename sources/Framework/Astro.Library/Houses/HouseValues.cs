using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{
    /// <summary>
    /// Valeurs de calcul d'une maison
    /// </summary>
    public class HouseValues
    {
        /// <summary>
        /// Numéro de lam maison
        /// </summary>
        public int House { get; set; }

        /// <summary>
        /// Nom de la maison
        /// </summary>
        public String HouseName { get; set; }

        /// <summary>
        /// Position de la cuspide
        /// </summary>
        public Double Cusp { get; set; }
    }
}
