using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.Services
{
    /// <summary>
    /// Astro service
    /// </summary>
    public interface IAstroService
    {
        /// <summary>
        /// Moteur astro
        /// </summary>
        AstroEngine AstroEngine { get; }
    }
}
