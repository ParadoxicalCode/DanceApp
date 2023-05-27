using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf.MVVM
{
    public class RelayCommand : ICommand
    {
        #region LOKALE VARIABLEN
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion

        #region KONSTRUKTOREN

        /// <summary>
        /// Erstellt ein Command, das immer ausgeführt werden kann.
        /// </summary>
        /// <param name="execute">Ausführen-Methode.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Erstellt ein neues Command.
        /// </summary>
        /// <param name="execute">Ausführen-Methode.</param>
        /// <param name="canExecute">Kann Ausgeführt werden-Methode.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region METHODEN

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return _canExecute == null ? true : _canExecute(parameters);
        }
        public void Execute(object parameters)
        {
            _execute(parameters);
        }

        #endregion

        #region EVENTS
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion
    }
}
