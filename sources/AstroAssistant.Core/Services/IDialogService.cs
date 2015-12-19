using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.Services
{

    /// <summary>
    /// Service de communication
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Exécute une demande de confirmation
        /// </summary>
        Task<DialogConfirmResult> Confirm(String title, String message, DialogConfirmType dialogType);

        /// <summary>
        /// Affichage d'un message d'erreur
        /// </summary>
        Task ShowError(Exception error, String title = null);
    }

    /// <summary>
    /// Type de dialogue de confirmation
    /// </summary>
    public enum DialogConfirmType
    {
        /// <summary>
        /// Oui/Non
        /// </summary>
        YesNo,
        /// <summary>
        /// Oui/Non/Annuler
        /// </summary>
        YesNoCancel
    }

    /// <summary>
    /// Résultat d'une dialogue de confirmation
    /// </summary>
    public enum DialogConfirmResult
    {
        /// <summary>
        /// Oui
        /// </summary>
        Yes,
        /// <summary>
        /// Non
        /// </summary>
        No,
        /// <summary>
        /// Annuler
        /// </summary>
        Cancel
    }

}
