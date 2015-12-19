using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.Services
{

    /// <summary>
    /// Service de gestion de fichier
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Sélectionne un fichier pour la lecture d'un thème
        /// </summary>
        Task<FileInformation> OpenLoadAsNatalChart();

        /// <summary>
        /// Ouvre un fichier pour une lecture
        /// </summary>
        Task<FileInformation> OpenLoadNatalChart(String fileName);

        /// <summary>
        /// Sélectionne un fichier pour l'enregistrement d'un thème
        /// </summary>
        Task<FileInformation> OpenSaveAsNatalChart();

        /// <summary>
        /// Ouvre un fichier pour un enregistrement
        /// </summary>
        Task<FileInformation> OpenSaveNatalChart(String fileName);

    }

}
