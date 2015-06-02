using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.ViewModels
{

    /// <summary>
    /// ViewModel principal
    /// </summary>
    public class MainViewModel : AppViewModel
    {
        /// <summary>
        /// Création d'un nouveau fournisseur d'éphéméride
        /// </summary>
        protected override Astro.IEphemerisProvider CreateEphemerisProvider()
        {
            if (SwephEpheNetProvider == null)
            {
                SwephEpheNetProvider = new Astro.SwissEphNet.SwissEphNetProvider();
            }
            return SwephEpheNetProvider;
        }

        /// <summary>
        /// Fournisseur d'éphéméride Swiss Ephemeris
        /// </summary>
        public Astro.SwissEphNet.SwissEphNetProvider SwephEpheNetProvider { get; private set; }
    }

}
