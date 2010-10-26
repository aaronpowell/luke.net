using System;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Practices.Unity.Utility;

// This is borrowed from Magellan Framework found at http://code.google.com/p/magellan-framework/
namespace Luke.Net.Infrastructure
{
    /// <summary>
    /// An <see cref="ICommand"/> that invokes a delegate on execution.
    /// </summary>
    public class RelayCommand : RelayCommand<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public RelayCommand(Action execute)
            : base(x => execute())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
            : base(x => execute(), x => canExecute())
        {
        }
    }
    
    /// <summary>
    /// 
    /// A generic <see cref="ICommand"/> that invokes a delegate on execution.
    /// </summary>
    public class RelayCommand<TArgument> : ICommand
    {
        private readonly Action<TArgument> _execute;
        private readonly Func<TArgument, bool> _canExecute;
        private EventHandler _canExecuteChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;TArgument&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public RelayCommand(Action<TArgument> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;TArgument&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public RelayCommand(Action<TArgument> execute, Func<TArgument, bool> canExecute)
        {
            Guard.ArgumentNotNull(execute, "execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (parameter == null && typeof(TArgument).IsValueType)
                return false;

            return _canExecute == null ? true : _canExecute((TArgument)parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChanged += value;
#if !SILVERLIGHT
                CommandManager.RequerySuggested += value;
#endif
            }
            remove
            {
                _canExecuteChanged -= value;
#if !SILVERLIGHT
                CommandManager.RequerySuggested -= value;
#endif
            }
        }

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = _canExecuteChanged;
            if (handler != null) handler(this, new EventArgs());
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            if (parameter == null && typeof(TArgument).IsValueType)
                return;

            _execute((TArgument)parameter);
        }
    }
}
