using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using ACEOMM.UI.Interfaces;
using ACEOMM.UI.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ACEOMM.UI.View
{
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }

        private void OnTreeViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            if (((TreeViewItem)e.Source).Header == vm.CurrentTreeEntity)
                vm.EditEntity(vm.CurrentTreeEntity);
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        #region IMainView
        public bool EditBusiness(Business entity, List<Product> products, List<Country> countries, List<Livery> liveries)
        {
            var window = new BusinessWindow()
            {
                Owner = this
            };
            (window.DataContext as BusinessWindowViewModel).Initialize(entity, products, countries, liveries);
            return window.ShowDialog().Value;
        }

        public bool EditMod(Mod entity, List<Business> businesses)
        {
            var window = new ModWindow()
            {
                Owner = this
            };
            (window.DataContext as ModWindowViewModel).Initialize(entity, businesses);
            return window.ShowDialog().Value;
        }

        public bool EditProduct(Product entity, List<Franchise> franchises)
        {
            var window = new ProductWindow()
            {
                Owner = this
            };
            (window.DataContext as ProductWindowViewModel).Initialize(entity, franchises);
            return window.ShowDialog().Value;
        }
        #endregion

        #region IView
        public void ShowMessage(string text)
        {
            MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string text)
        {
            Dispatcher.Invoke(() => MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Error));
        }

        public bool AskYesNoConfirmation(string text)
        {
            return MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public bool AskOkCancelConfirmation(string text)
        {
            return MessageBox.Show(text, App.Current.MainWindow.Title, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            var vm = (MainWindowViewModel)DataContext;
            e.Cancel = !vm.CanExit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            vm.CheckForUpdates();
        }
    }
}
