using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro.Library
{
    /// <summary>
    /// Astrology engine
    /// </summary>
    public class AstroEngine : IDisposable
    {
        IEphemerisProvider _EphemerisProvider;

        #region Ctors & Dest

        /// <summary>
        /// Nouveau moteur
        /// </summary>
        public AstroEngine(IEphemerisProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            _EphemerisProvider = provider;
        }

        /// <summary>
        /// Libération interne des ressources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _EphemerisProvider.Dispose();
        }

        /// <summary>
        /// Libération des ressources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Fournisseur d'éphémérides
        /// </summary>
        public IEphemerisProvider EphemerisProvider { get { return _EphemerisProvider; } }

        #endregion

    }

}
