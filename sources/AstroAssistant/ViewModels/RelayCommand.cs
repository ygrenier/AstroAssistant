using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AstroAssistant.ViewModels
{

    /// <summary>
    /// Commande
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action _Execute = null;
        private Func<bool> _CanExecute = null;

        /// <summary>
        /// Création d'une nouvelle commande
        /// </summary>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Création d'une nouvelle commande
        /// </summary>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _Execute = execute;
            _CanExecute = canExecute;
        }

        /// <summary>
        /// Provoque CanExecuteChanged
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Exécute de la commande
        /// </summary>
        public void Execute(object parameter)
        {
            _Execute();
        }

        /// <summary>
        /// Détermine si on peut exécuter la commande
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _CanExecute != null ? _CanExecute() : true;
        }

        /// <summary>
        /// Evénement provoqué lorsque l'état CanExecute à changé
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_CanExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_CanExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

    }

}
