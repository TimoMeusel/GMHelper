using System;
using System.Windows.Input;

namespace GM.ViewModel
{
    /// <summary>
    ///     Base class for general commands
    /// </summary>
    public abstract class BaseCommand: ICommand
    {
        #region Fields

        private EventHandler _canExecuteChangedEventHandler;

        #endregion

        /// <summary>
        ///     Validation of the parameters
        /// </summary>
        public virtual bool CanExecute (object parameter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Executes the command
        /// </summary>
        public void Execute (object parameter)
        {
            var currentCursor = Mouse.OverrideCursor;

            try
            {
                ExecuteInternal(parameter);
            }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
            }

        }

        protected virtual void ExecuteInternal (object parameter)
        {
            
        }

        /// <summary>
        ///     Populates the can execute state
        /// </summary>
        public virtual event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChangedEventHandler += value;
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                _canExecuteChangedEventHandler -= value;
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
