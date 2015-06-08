using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro.SwissEphNet
{

    /// <summary>
    /// Erreur lors d'un appel à SwissEphNet
    /// </summary>
    public class SwissEphNetError : Exception
    {
        /// <summary>
        /// Création d'un nouveau message
        /// </summary>
        public SwissEphNetError(String message)
            : base(message)
        {
        }
    }

}
