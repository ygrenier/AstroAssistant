using AstroAssistant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.ViewModels
{
    /// <summary>
    /// Base Application ViewModel
    /// </summary>
    public class AppViewModel : ViewModel, AstroAssistant.ViewModels.IAppViewModel
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
        /// Provoque un appel protégé
        /// </summary>
        async Task<bool> CallProtected(Func<Task<bool>> call)
        {
            Exception error = null;
            try
            {
                // On provoque l'appel
                return await call();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            if (error != null)
            {
                await DialogService.ShowError(error);
            }
            return false;
        }

        /// <summary>
        /// Provoque un appel protégé avec sauvegarde si les modifications d'un thème n'ont pas été enregistré
        /// </summary>
        async Task<bool> CallProtectedWithDirtySaveIfRequired(Func<Task<bool>> call)
        {
            Exception error = null;
            try
            {
                // Si le thème actuel est à sauvegarder
                if (CurrentNatalChart.IsDirty)
                {
                    // On demande à l'utilisateur ce qu'il veut faire
                    var cr = await DialogService.Confirm(
                        AstroAssistant.Resources.Locales.SaveChangesDialogTitle,
                        AstroAssistant.Resources.Locales.SaveChangesDialogMessage,
                        DialogConfirmType.YesNoCancel);
                    if (cr == DialogConfirmResult.Cancel) return false;
                    if (cr == DialogConfirmResult.Yes)
                    {
                        if (!await CurrentNatalChart.Save())
                        {
                            return false;
                        }
                    }
                }
                // On provoque l'appel
                return await call();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            if (error != null)
            {
                await DialogService.ShowError(error);
            }
            return false;
        }

        /// <summary>
        /// Ouverture d'un nouveau thème astral vierge
        /// </summary>
        public Task<bool> NewNatalChart()
        {
            return CallProtectedWithDirtySaveIfRequired(() => Task.Run(() => {
                // On réinitialise le thème
                CurrentNatalChart.Reset();
                return true;
            }));
        }

        /// <summary>
        /// Chargement d'un thème astral
        /// </summary>
        /// <returns></returns>
        public Task<bool> LoadNatalChart()
        {
            return CallProtectedWithDirtySaveIfRequired(() => Task.Run(() => CurrentNatalChart.LoadFromFile()));
        }

        /// <summary>
        /// Enregistrement d'un thème astral
        /// </summary>
        public Task<bool> SaveNatalChart()
        {
            return CallProtectedWithDirtySaveIfRequired(() => Task.Run(() => CurrentNatalChart.Save()));
        }

        /// <summary>
        /// Enregistrement d'un thème astral sous un autre nom
        /// </summary>
        public Task<bool> SaveAsNatalChart()
        {
            return CallProtectedWithDirtySaveIfRequired(() => Task.Run(() => CurrentNatalChart.SaveAs()));
        }

        /// <summary>
        /// Provoque le calcul du thème astral
        /// </summary>
        /// <returns></returns>
        public Task CalculateNatalChart()
        {
            return CallProtected(() => Task.Run(() => CurrentNatalChart.Calculate()));
        }

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
        public INatalChartViewModel CurrentNatalChart
        {
            get { return _CurrentNatalChart; }
            private set { SetProperty(ref _CurrentNatalChart, value, () => CurrentNatalChart); }
        }
        private INatalChartViewModel _CurrentNatalChart;

    }

}
