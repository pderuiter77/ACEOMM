using ACEOMM.UI.Interfaces;
using System;
using System.Windows.Input;

namespace ACEOMM.UI.Commands
{
    public class RelayCommandOfT<T> : ICommand<T>
    {
        private Predicate<T> _canExecute;
        private Action<T> _execute;

        public RelayCommandOfT(Predicate<T> canExecute, Action<T> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        public RelayCommandOfT(Action<T> execute)
            : this((T) => { return true; }, execute)
        { }

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(T parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(T parameter)
        {
            _execute(parameter);
        }
    }
}
