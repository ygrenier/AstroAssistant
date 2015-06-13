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
        public MainViewModel(Services.IAstroService astroService)
            : base(astroService)
        {
        }

    }

}
