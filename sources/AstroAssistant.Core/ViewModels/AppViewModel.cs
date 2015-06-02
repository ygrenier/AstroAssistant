using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroAssistant.ViewModels
{
    /// <summary>
    /// Base Application ViewModel
    /// </summary>
    public abstract class AppViewModel : ViewModel, IDisposable
    {

        /// <summary>
        /// Internal release resources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Close();
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

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
        /// Close the viewmodel
        /// </summary>
        public void Close()
        {
            if (AstroEngine != null)
            {
                AstroEngine.Dispose();
                AstroEngine = null;
            }
        }

        /// <summary>
        /// Current Astro Engine
        /// </summary>
        public Astro.AstroEngine AstroEngine { get; private set; }

    }

}
