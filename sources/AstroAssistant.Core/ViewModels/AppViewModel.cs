using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroAssistant.ViewModels
{
    /// <summary>
    /// Base Application ViewModel
    /// </summary>
    public class AppViewModel : ViewModel
    {
        /// <summary>
        /// Création d'un nouveau ViewModel
        /// </summary>
        public AppViewModel(Services.IAstroService astroService)
        {
            if (astroService == null) throw new ArgumentNullException("astroService");
            this.AstroService = astroService;
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Service Astro
        /// </summary>
        public Services.IAstroService AstroService { get; private set; }
    }

}
