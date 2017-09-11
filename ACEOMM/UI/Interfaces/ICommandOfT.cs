using System.Windows.Input;

namespace ACEOMM.UI.Interfaces
{
    public interface ICommand<T> : ICommand
    {
        bool CanExecute(T parameter);
        void Execute(T parameter);
    }
}
