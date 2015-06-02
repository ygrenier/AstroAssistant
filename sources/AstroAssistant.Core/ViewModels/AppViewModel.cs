using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroAssistant.ViewModels
{
    /// <summary>
    /// Base Application ViewModel
    /// </summary>
    public abstract class AppViewModel : ViewModel
    {

        /// <summary>
        /// Ephemeris provider creator
        /// </summary>
        protected abstract Astro.IEphemerisProvider CreateEphemerisProvider();

        /// <summary>
        /// Initialize the ViewModel
        /// </summary>
        public void Initialize()
        {
            // Check if it's already initialized
            if (AstroEngine != null) return;
            // Create the engine
            AstroEngine = new Astro.AstroEngine(CreateEphemerisProvider());
        }

        /// <summary>
        /// Current Astro Engine
        /// </summary>
        public Astro.AstroEngine AstroEngine { get; private set; }
        
    }

}
