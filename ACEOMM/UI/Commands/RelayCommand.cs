using System;
using System.Windows.Input;

namespace ACEOMM.UI.Commands
{
    public class RelayCommand : ICommand
    {
        private Func<bool> _canExecute;
        private Action _execute;

        public RelayCommand(Func<bool> canExecute, Action execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        public RelayCommand(Action execute)
            : this(() => { return true; }, execute)
        { }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute()
        {
            return _canExecute();
        }

        public void Execute()
        {
            _execute();
        }
    }
}
