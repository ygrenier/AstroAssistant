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
    public class MainViewModel : AppViewModel, AstroAssistant.ViewModels.IMainViewModel
    {

        /// <summary>
        /// Nouveau ViewModel principal
        /// </summary>
        public MainViewModel(Services.IAstroService astroService, Services.IDialogService dialogService, Services.IResolverService resolverService)
            : base(astroService, dialogService, resolverService)
        {
            NewNatalChartCommand = new RelayCommand(async () => {
                await NewNatalChart();
            });
            LoadNatalChartCommand = new RelayCommand(async () => {
                await LoadNatalChart();
            });
            SaveNatalChartCommand = new RelayCommand(async () => {
                await SaveNatalChart();
            });
            SaveAsNatalChartCommand = new RelayCommand(async () => {
                await SaveAsNatalChart();
            });
            CalculateNatalChartCommand = new RelayCommand(async () => {
                await CalculateNatalChart();
            });
        }

        /// <summary>
        /// Commande pour créer un nouveau thème astral
        /// </summary>
        public RelayCommand NewNatalChartCommand { get; private set; }

        /// <summary>
        /// Commande pour le chargement d'un thème astral
        /// </summary>
        public RelayCommand LoadNatalChartCommand { get; private set; }

        /// <summary>
        /// Commande pour l'enregistrement d'un thème astral
        /// </summary>
        public RelayCommand SaveNatalChartCommand { get; private set; }

        /// <summary>
        /// Commande pour l'enregistrement sous un autre nom d'un thème astral
        /// </summary>
        public RelayCommand SaveAsNatalChartCommand { get; private set; }

        /// <summary>
        /// Commande pour recalculer le thème astral
        /// </summary>
        public RelayCommand CalculateNatalChartCommand { get; private set; }

    }

}
