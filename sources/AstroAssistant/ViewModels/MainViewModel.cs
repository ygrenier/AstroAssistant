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
    public class MainViewModel : AppViewModel
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
        }

        /// <summary>
        /// Commande pour créer un nouveau thème astral
        /// </summary>
        public RelayCommand NewNatalChartCommand { get; private set; }

    }

}
