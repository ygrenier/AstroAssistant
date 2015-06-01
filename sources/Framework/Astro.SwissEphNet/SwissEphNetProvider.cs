using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sweph = global::SwissEphNet;

namespace Astro.SwissEphNet
{

    /// <summary>
    /// SwissEphNet Ephemeris Provider
    /// </summary>
    public class SwissEphNetProvider : IEphemerisProvider
    {
        Sweph.SwissEph _Sweph;

        /// <summary>
        /// Create SwissEphNet provider
        /// </summary>
        public SwissEphNetProvider()
        {
            _Sweph = new Sweph.SwissEph();
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            _Sweph.Dispose();
        }

    }

}
