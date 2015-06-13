using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.Services
{

    /// <summary>
    /// Service Astro par défaut
    /// </summary>
    public class AstroService : IAstroService, IDisposable
    {

        /// <summary>
        /// Création d'un nouveau service
        /// </summary>
        public AstroService(IEphemerisService ephemeris)
        {
            // Create the engine
            AstroEngine = new Astro.AstroEngine(ephemeris);
        }

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (AstroEngine != null)
                {
                    AstroEngine.Dispose();
                    AstroEngine = null;
                }
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Current Astro Engine
        /// </summary>
        public AstroEngine AstroEngine { get; private set; }

    }

}
