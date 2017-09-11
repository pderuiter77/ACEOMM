using ACEOMM.UI.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ACEOMM.UI.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        protected IView View {get; private set;}

        public ViewModel(IView view)
        {
            View = view;
        }

        protected void HandleError(string errorMessage)
        { 
            
        }
    }
}
