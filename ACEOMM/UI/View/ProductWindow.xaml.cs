using ACEOMM.UI.Interfaces;
using ACEOMM.UI.ViewModel;
using System.Windows;

namespace ACEOMM.UI.View
{
    public partial class ProductWindow : Window, IView
    {
        public ProductWindow()
        {
            InitializeComponent();
            DataContext = new ProductWindowViewModel(this);
        }

        #region IView
        public void ShowMessage(string text)
        {
            MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string text)
        {
            MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool AskYesNoConfirmation(string text)
        {
            return MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public bool AskOkCancelConfirmation(string text)
        {
            return MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public bool? AskYesNoCancelConfirmation(string text)
        {
            switch (MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return null;
            }
        }
        #endregion
    }
}
