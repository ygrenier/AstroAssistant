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
        public AppViewModel(
            Services.IAstroService astroService,
            Services.IDialogService dialogService,
            Services.IResolverService resolverService
            )
        {
            if (astroService == null) throw new ArgumentNullException("astroService");
            if (dialogService == null) throw new ArgumentNullException("dialogService");
            if (resolverService == null) throw new ArgumentNullException("resolverService");
            this.AstroService = astroService;
            this.DialogService = dialogService;
            CurrentNatalChart = resolverService.CreateViewModel<NatalChartViewModel>();
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Service Astro
        /// </summary>
        public Services.IAstroService AstroService { get; private set; }

        /// <summary>
        /// Service de dialogues
        /// </summary>
        public Services.IDialogService DialogService { get; private set; }

        /// <summary>
        /// Thème en cours
        /// </summary>
        public NatalChartViewModel CurrentNatalChart
        {
            get { return _CurrentNatalChart; }
            private set { SetProperty(ref _CurrentNatalChart, value, () => CurrentNatalChart); }
        }
        private NatalChartViewModel _CurrentNatalChart;

    }

}
