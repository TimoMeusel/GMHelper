using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GM.ViewModel
{
    /// <summary>
    ///     A command whose sole purpose is to
    ///     relay its functionality to other
    ///     objects by invoking delegates. The
    ///     default return value for the CanExecute
    ///     method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Constants and Fields

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Creates a new instance of this class
        /// </summary>
        public RelayCommand(ICommand cmd)
        {
            _execute = cmd.Execute;
            _canExecute = cmd.CanExecute;
        }

        /// <summary>
        ///     Creates a new instance of this class
        /// </summary>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        ///     Creates a new instance of this class
        /// </summary>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     An event that is fired when CanExecute changes
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Determines if the command can be executed or not
        /// </summary>
        /// <returns> True if the command can be executed, false otherwise </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        ///     Executes the command with the passed parameter
        /// </summary>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        
        #endregion
    }
}
