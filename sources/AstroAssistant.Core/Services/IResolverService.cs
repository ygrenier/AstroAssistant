using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.Services
{
    /// <summary>
    /// Service de résolution de dépendances
    /// </summary>
    public interface IResolverService
    {

        /// <summary>
        /// Récupération d'un service
        /// </summary>
        T GetService<T>() where T : class;

        /// <summary>
        /// Création d'un viewmodel
        /// </summary>
        T CreateViewModel<T>() where T : ViewModels.ViewModel;

    }
}
