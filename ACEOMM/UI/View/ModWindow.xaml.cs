using ACEOMM.UI.Interfaces;
using ACEOMM.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ACEOMM.UI.View
{
    public partial class ModWindow : Window, IView
    {
        public ModWindow()
        {
            InitializeComponent();
            DataContext = new ModWindowViewModel(this);
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
